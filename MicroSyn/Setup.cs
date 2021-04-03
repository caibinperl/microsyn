using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MirSyn
{
    public class Setup
    {
        Data data;       
        //Store <mir, list>
        Dictionary<string, ElementList> mirRegions = new Dictionary<string, ElementList>();
	    //<String, ElementList>
        List<Cluster> clusters;
        Dictionary<string, ResultSeg> resultsegs;

        public Setup(Data data)
        {
            this.data = data;
            clusters = this.data.getClusters();
            resultsegs = this.data.getResultSegs();
        }

        public void blast()
        {
            string formatdb = data.getFormatdb();
            string blastall = data.getBlastall();
            string seq_file = data.getSeqFile();
            string formatdb_argu = "-i " + seq_file + " -o F -p T";       
            string blastall_argu = "-F F -p blastp"
                                          + " -i " + seq_file
                                          + " -d " + seq_file
                                          + " -o " +  data.getBlastOut();        
            ExternalWrapper formatdb_ew = new ExternalWrapper(formatdb, formatdb_argu);
            formatdb_ew.runXternal();
            ExternalWrapper blastall_ew = new ExternalWrapper(blastall, blastall_argu);
            blastall_ew.runXternal();                        
        }

        public void parseBlast() 
        {
            try
            {
                StreamWriter sw = new StreamWriter(
										new FileStream(data.getBlastTable(), FileMode.Create));
                StreamReader sr = new StreamReader(
										new FileStream(data.getBlastOut(), FileMode.Open));
                bool result_start = false;
                bool hit_start = false;
                bool eof = false;  
                bool read = true;
                Match m;                
                string line = null;
                line = sr.ReadLine();
                if (line == null)
                {
                    MessageBox.Show("The blast output is empty!");
                    Application.Exit();
                }
                string queryName = null;
                // *************** START OF BIG LOOP ****************                
                while (!eof)
                {                     
                    if(read)
                    {
                        line = sr.ReadLine();
                        if (Regex.Match(line, @"Query= (\S+)").Success)
                        {   
                            result_start = true;
                            read = false;
                        }                     
                    }                       
                    if (result_start)
                    {
                        m = Regex.Match(line, @"Query= (\S+)");
                        queryName = m.Groups[1].Value;
                        while(true)
                        {
                            line = sr.ReadLine();
                            if (Regex.Match(line, @">(\S+)").Success)
                            {                                
                                hit_start = true;
                                break;
                            }
                        }
                        result_start = false;                        
                    }
                    if (hit_start)
                    { 
                        m = Regex.Match(line, @">(\S+)");
                        string subjectName = m.Groups[1].Value;
                        sr.ReadLine();
                        sr.ReadLine();
                        sr.ReadLine();
                        line = sr.ReadLine();
                        m = Regex.Match(line, @"Identities = (\d+)/(\d+)");
                        if (!m.Success)
                        {
                            MessageBox.Show("There are errors in" + line);
                        }
                        int len = Int32.Parse(m.Groups[2].Value);
                        double iden = Double.Parse(m.Groups[1].Value) / Double.Parse(m.Groups[2].Value);
                        sr.ReadLine();
                        line = sr.ReadLine();
                        StringBuilder querySeq = new StringBuilder(5000);
                        StringBuilder subjectSeq = new StringBuilder(5000);
                        m = Regex.Match(line, @"Query: +(\d+) +([\w\*-]+) +(\d+)");
                        if (!m.Success) MessageBox.Show(line);
                        int queryStart = Int32.Parse(m.Groups[1].Value);
                        int queryEnd = Int32.Parse(m.Groups[3].Value);
                        querySeq.Append(m.Groups[2].Value);
                        sr.ReadLine();
                        line = sr.ReadLine();
                        m = Regex.Match(line, @"Sbjct: +(\d+) +([\w\*-]+) +(\d+)");
                        if (!m.Success) MessageBox.Show(line);
                        int subjectStart = Int32.Parse(m.Groups[1].Value);
                        int subjectEnd = Int32.Parse(m.Groups[3].Value);
                        subjectSeq.Append(m.Groups[2].Value);
                        sr.ReadLine();                        
                        while ((line = sr.ReadLine()).Trim().Length != 0)
                        {
                            m = Regex.Match(line, @"Query: +\d+ +([\w\*-]+) +(\d+)");                           
                            queryEnd = Int32.Parse(m.Groups[2].Value);
                            querySeq.Append(m.Groups[1].Value);
                            sr.ReadLine();
                            line = sr.ReadLine();
                            m = Regex.Match(line, @"Sbjct: +\d+ +([\w\*-]+) +(\d+)");
                            subjectEnd = Int32.Parse(m.Groups[2].Value);
                            subjectSeq.Append(m.Groups[1].Value);
                            sr.ReadLine();
                        }
                        if (iden >= 0.3 && len >= 100)
                        {
                            sw.Write(queryName + "\t" + subjectName + "\n");
                        }
                        hit_start = false;
                        while(true)
                        {
                            line = sr.ReadLine();
                            if (line == null)
                            {
                                eof = true;
                                break;
                            }
                            if (Regex.Match(line, @">(\S+)").Success)
                            {
                                hit_start = true;
                                break;
                            }
                            if(Regex.Match(line, @"Query= (\S+)").Success)
                            {
                                result_start = true;
                                break;
                            }                            
                        }
                    }                    
                }
                // *************** END OF BIG LOOP ****************
                sr.Close();
                sw.Flush();
                sw.Close();
            } catch(IOException e ){
                MessageBox.Show(e.Message);
            }       
        }
                
        //Read blast table to set gene object homologys
        public void mapGenes() 
        {
            List<string[]> homologys = data.getHomologys();
            Dictionary<string, Gene> genomeGenes = new Dictionary<string, Gene>();
            List<ElementList> genomelists = data.getMirRegions();
            
            /*Copy gene name and store gene object in genomeGenes <ID, Gene>
            * genomeGenes contains all the genes
            */
            for(int i = 0; i < genomelists.Count; i++)
            {
                List<Element> elements = genomelists[i].getElements();

                for(int j = 0; j < elements.Count; j++)
                {
                    Gene gene = elements[j].getGene();
                    string id = gene.getID();
                    if (!genomeGenes.ContainsKey(id))
                    {
                        genomeGenes.Add(gene.getID(), gene);
                    }
                }
            }
            //read blast table file
            try 
            {
                string line = null;
                StreamReader sr = new StreamReader(
										new FileStream(data.getBlastTable(), FileMode.Open));
                while ( (line = sr.ReadLine()) != null )
							 	{
                    string[] genes = Regex.Split(line, "\\s+");
                    string gene_a = "", gene_b = "";
                    if(genes.Length == 2)
                    {
                        gene_a = genes[0];
                        gene_b = genes[1];
                    }
                    if ( gene_a.Length != 0 && gene_b.Length != 0 && !gene_a.Equals(gene_b))
				    {
                        //only insert when not the same gene
                        if (genomeGenes.ContainsKey(gene_a) && genomeGenes.ContainsKey(gene_b))
											 	{
                            (genomeGenes[gene_a]).addHomologys(gene_b);
                            (genomeGenes[gene_b]).addHomologys(gene_a);
                        }
                    }
                }
                sr.Close();
            } catch (IOException e) {
                MessageBox.Show(e.Message);
            }
            //mir homologys     
            foreach (string[] pair in homologys)
            {                
                string gene_a = pair[0];
                string gene_b = pair[1];
                if ( gene_a.Length != 0 && gene_b.Length != 0 && !gene_a.Equals(gene_b)) 
                {
                    //only insert when not the same gene
                    if (genomeGenes.ContainsKey(gene_a) && genomeGenes.ContainsKey(gene_b)) {
                        genomeGenes[gene_a].addHomologys(gene_b);
                        genomeGenes[gene_b].addHomologys(gene_a);
                    }
                }
            }         
	    }

        //remaps pairs and indirect pairs for every genelist on one gene
			public void remapTandems() 
      {
      	List<ElementList> genomelists = data.getMirRegions();
		    for (int i = 0; i < genomelists.Count; i++)
        {
           genomelists[i].remapTandemsHomologys(data.getTandemGap());
        }
      }   

        public void Detection()
        {
            createMirRegion();
            level2Detection();
            sortClusters(data.getClusters());       
            findResultSeg();
            homologElements(); 
        }
        
        private void sortClusters(List<Cluster> clusters)
        {
            for (int i = 1; i < clusters.Count; i++)
            {
                Cluster tmp = clusters[i];
                int j = i;
                while (j > 0 && tmp.getCountHomologyPoints() > clusters[j-1].getCountHomologyPoints())
                {
                    clusters[j] = clusters[j-1];
                    j--;
                }
                clusters[j] =  tmp;
            }
        }

        private void level2Detection()
        {
            List<string> mirs = new List<string>();
            Dictionary<string, ElementList>.KeyCollection kc = mirRegions.Keys;
            foreach(string key in kc){
                mirs.Add(key);                
            }        
            for (int i = 0; i < mirs.Count - 1; i++) 
            {
                for(int j = i + 1; j < mirs.Count; j++)
                {
                    string x_mir = mirs[i];
                    string y_mir = mirs[j];
                    Matrix matrix = new Matrix(
												data, x_mir, y_mir, mirRegions[x_mir], mirRegions[y_mir]);             
                    matrix.buildMatrix();                   
                    matrix.run();
                    addCluster(matrix.getCluster());
                }
            }      
        }
 
        private void addCluster(Cluster mplicon)
        {      
            if(mplicon != null)
                clusters.Add(mplicon);            
        }  

        public void createMirRegion()
        {
            List<ElementList> genomeLists = data.getMirRegions();
            for(int i = 0; i < genomeLists.Count; i++)
            {
                Gene mir = genomeLists[i].getMirGene();
                if(mir != null)
                {
                    mirRegions.Add(mir.getID(), genomeLists[i]);
                }
                else
                {
                    MessageBox.Show("There are wrong when search mir in a mir region: " + 
												genomeLists[i].getListName());
                    Application.Exit();
                }
            }
        }

        public void findResultSeg()
        {
            for(int i = 0; i < clusters.Count; i++)
            {
                Cluster cluster = clusters[i];
                string mirxID = cluster.getMirX();
                string miryID = cluster.getMirY();
                int begin_x = cluster.getBeginX();
                int end_x = cluster.getEndX();
                int begin_y = cluster.getBeginY();
                int end_y = cluster.getEndY();            
                if(resultsegs.ContainsKey(mirxID))
                {
                    ResultSeg resultseg = resultsegs[mirxID];
                    int max_index = resultseg.getMaxCoord();
                    int min_index = resultseg.getMinCoord();                    
                    if( begin_x < min_index)
                    {
                        resultseg.setMinCoord(begin_x);
                    }
                    if(end_x > max_index)
                    {
                        resultseg.setMaxCoord(end_x);
                    }
                } 
                else
                {
                    resultsegs[mirxID] = new ResultSeg(mirxID, cluster.getXObject(),begin_x, end_x);
                }

                if(resultsegs.ContainsKey(miryID))
                {
                    ResultSeg resultseg = resultsegs[miryID];
                    int max_index = resultseg.getMaxCoord();
                    int min_index = resultseg.getMinCoord();

                    if( begin_y < min_index)
                    {
                        resultseg.setMinCoord(begin_y);
                    }
                    if(end_y > max_index)
                    {
                        resultseg.setMaxCoord(end_y);
                    }
                } 
                else 
                {
                    resultsegs[miryID] = new ResultSeg( miryID, cluster.getYObject(), begin_y, end_y);
                }
            }

            //updata seq          
            foreach(KeyValuePair<string, ResultSeg> pair in resultsegs)
            {
                pair.Value.createSeq();               
            }
        }

        public void homologElements()
        {
            for(int i = 0; i < clusters.Count; i++)
            {
                Cluster cluster = clusters[i];
                string mirxID = cluster.getMirX();
                string miryID = cluster.getMirY();
                ElementList seg_x = resultsegs[mirxID].getSeg();
                ElementList seg_y = resultsegs[miryID].getSeg();
                List<BaseCluster> baseclusters = cluster.getBaseClusters();
                for(int j = 0; j < baseclusters.Count; j++)
                {
                    BaseCluster basecluster = baseclusters[j];
                    List<HomologyPoint> homologyPoints = basecluster.getHomologyPoints();                  
                    foreach (HomologyPoint point in homologyPoints)
                    {                        
                        Element e_x = seg_x.getElementByGeneID(point.getGeneX().getID());
                        Element e_y = seg_y.getElementByGeneID(point.getGeneY().getID());
                        if( e_x != null && e_y != null)
                        {
                            e_x.addHomologyElement(e_y);
                            e_y.addHomologyElement(e_x);
                        }                    
                    }
                }
            }
        }

        //Set
        public void resetCluster(){ clusters = new List<Cluster>(); }

        //Get            
        public Data getData() { return data; }       
    }
}
