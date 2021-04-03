using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace MirSyn
{
    public class Data
    {
        string msyFile;        
        string seq_file;
        string blast_out;
        string blast_table;        
        string listFile;
        string formatdb;
        string blastall;
        string mlcFile;
        string codemlInput;
        string codeml;
        string codeml_ctl;
        string mldsFile;

        string help_file;

        bool isLoadMsy = false;
        bool isDetected = false;
       
        int gap_size = 30;
        int cluster_gap = 35;
        int tandem_gap = 2;
        double q_value = 0.8;
        int homologypoints = 3;
        double prob_cutoff = 0.01;       
        
        List<ElementList> mirRegions = new List<ElementList>();
        List<string[]> homologys = new List<string[]>();
        HashSet<string> family = new HashSet<string>();
        List<Gene> family_genes = new List<Gene>(); 
        
        //Store result clusters       
        List<Cluster> clusters = new List<Cluster>();
        Dictionary<string, ResultSeg> resultsegs = new Dictionary<string, ResultSeg>();

        public void setURL() 
        {
            string setup_path = Application.StartupPath;           
            //string data_path = setup_path + "\\data";
            string data_path = @"data";
            //string bin_path = setup_path + "\\bin";
            string bin_path = @"bin"; 
            if (Directory.Exists(data_path))
            {
                string[] files = Directory.GetFiles(data_path);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            else
            {
                Directory.CreateDirectory(data_path);
            }                        
            seq_file = data_path + @"\pep.ini";
            blast_out = data_path + @"\blastout.ini";
            blast_table = data_path + @"\blasttable.ini";
            blast_table = data_path + @"\blasttable.ini";

            formatdb = bin_path + @"\formatdb.exe";
            blastall = bin_path + @"\blastall.exe";
            codeml = bin_path + @"\codeml.exe";

            codeml_ctl = data_path + @"\codeml.ctl";
            mlcFile = data_path + @"\mlc";
            codemlInput = data_path + @"\codeml.input";

            mldsFile = @"2ML.dS";

            help_file = @"help.chm";
           
            try
            {
                StreamWriter sw = new StreamWriter(new FileStream(codeml_ctl, FileMode.Create));
                sw.Write(
                    "seqfile = " + codemlInput + "\n" +
                    "outfile = " + mlcFile + "\n" + 
                    "noisy = 3\n" +
                    "verbose = 0\n" +                   
                    "seqtype = 1\n" +
                    "CodonFreq = 2\n" +
                    "clock = 0\n" +
                    "model = 0\n" +
                    "NSsites = 0\n" +
                    "icode = 0\n"                     
                    );
                sw.Flush();
                sw.Close();
            }
            catch(IOException e)
            {
                MessageBox.Show(e.Message);
            }
        

        }

        public void clearMsy()
        {
            msyFile = null;
            isLoadMsy = false;
            isDetected = false;
            family.Clear();
            homologys.Clear();
            mirRegions.Clear();
            clusters.Clear();
            resultsegs.Clear();
            setURL();            
        }

        public bool loadMsyFile()
        {           
            try
            {
                StreamWriter sw = new StreamWriter(new FileStream(seq_file, FileMode.Create));                
                XmlDocument doc = new XmlDocument();
                doc.Load(msyFile);
                // 设定XmlNodeReader对象来打开XML文件                
                XmlNodeList top_list = doc.DocumentElement.ChildNodes;
                foreach (XmlElement top in top_list)
                {
                    if (top.Name == "family")
                    {
                        //string species = element1.Attributes["species"].Value;
                        //得到该节点的子节点
                        XmlNodeList gene_list = top.ChildNodes;
                        foreach (XmlElement gene in gene_list)
                        {
                            string id = gene.Attributes["id"].Value;
                            string chr = gene.Attributes["chr"].Value;
                            int strand = Convert.ToInt32(gene.Attributes["strand"].Value);
                            int pos = Convert.ToInt32(gene.Attributes["pos"].Value);
                            Gene g = new Gene(id,chr,strand, pos);
                            family_genes.Add(g);
                            family.Add(id);                  
                        }
                    }
                    else if (top.Name == "neighbor")
                    {
                        string mir_id = top.Attributes["family_gene"].Value;
                        string list_name = mir_id;
                        List<Element> elements = new List<Element>();

                        XmlNodeList gene_list = top.ChildNodes;                        
                        foreach (XmlElement gene in gene_list) 
                        {
                            string id = gene.Attributes["id"].Value;
                            string chr = gene.Attributes["chr"].Value;
                            int strand = Convert.ToInt32(gene.Attributes["strand"].Value);
                            int pos = Convert.ToInt32(gene.Attributes["pos"].Value);
                            Gene gene_ele = new Gene(id, chr, pos, strand);
                            if (id.Equals(mir_id))
                            {
                                gene_ele.setIsMirRna(true);
                            }
                            elements.Add(new Element(gene_ele, strand));                            

                            XmlNodeList seq_list = gene.ChildNodes;
                            string seq = null;
                            foreach (XmlElement seq_ele in seq_list)
                            {
                                if (seq_ele.Name == "seq")
                                {
                                   seq = seq_ele.InnerText;                                   
                                }
                            }
                            
                            string pep = null;
                            if(seq != null)
                                pep = Translation.translate(id, seq);
                            if (pep != null && pep.Trim() != "")
                            {
                                sw.Write(">" + id + "\n");
                                sw.Write(pep + "\n");
                            }
                        }
                        mirRegions.Add(new ElementList(list_name, elements));
                    }
                    else
                    {
                        MessageBox.Show("Error in msy file!");
                        this.clearMsy();
                        return false;
                    }
                }
                sw.Flush();
                sw.Close();             
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            
            return true;
        }
           
        public void loadFamily(string line)
        {
            string[] geneNames = Regex.Split(line,"\\t+");
            for (int i = 2; i < geneNames.Length; i++)
            {
                family.Add(geneNames[i]);
            }
        }        

        public void loadHomologys()
        {
            for (int i = 0; i < family_genes.Count - 1; i++)
            {
                string gene_a = family_genes[i].getID();
                for (int j = i + 1; j < family_genes.Count; j++)
                {
                    string gene_b = family_genes[j].getID();
                    string[] pair = { gene_a, gene_b };
                    homologys.Add(pair);
                }
            }
        }
      
        
        public bool checkData(){
            if(msyFile ==null){
                MessageBox.Show("MYS file has not been loaded!");
                return false;
            }
            if(family.Count == 0){
                MessageBox.Show("Family genes is empty!");
                return false;
            }        
            if(mirRegions.Count == 0){
                MessageBox.Show("Genome lists is empty!");
                return false;
            }
            if(gap_size <= 0 ){
                MessageBox.Show("Gap size must > 0 !");
                return false;
            }   
            if(cluster_gap <= 0){
                MessageBox.Show("Clust Gap size must > 0 !");
                return false;
            }
            if(homologypoints <= 0){
                MessageBox.Show("Homologypoints size must > 0 !");
                return false;
            }         
            if(prob_cutoff <= 0){
                MessageBox.Show("Expected Value: must > 0 !");
                return false;
            }
            if(tandem_gap <= 0){
                MessageBox.Show("Tandem gap size must > 0 !");
                return false;
            }
            return true;
        }

        public void deleteHomologys() { homologys = null; }

        //Set
        public void setMsyFile(string msyFile) { this.msyFile = msyFile; }
        public void setListFile(string listFile) { this.listFile = listFile; }        
        public void setGapSize(int gap_size) { this.gap_size = gap_size; }
        public void setClusterGap(int cluster_gap) { this.cluster_gap = cluster_gap; }
        public void setTandemGap(int tandem_gap) { this.tandem_gap = tandem_gap; }
        public void setHomologyPoints(int homologypoints) { this.homologypoints = homologypoints; }
        public void setProbCutoff(double prob_cutoff) { this.prob_cutoff = prob_cutoff; }
        public void setIsLoadMsy(bool isLoadMsy) { this.isLoadMsy = isLoadMsy; }
        //public void setBlastTable(File blast_table){ this.blast_table = blast_table; }
        public void setIsDetected(bool isDetected) { this.isDetected = isDetected; }
      
        //Get       
        public string getSeqFile() { return seq_file; }
        public string getListFile() { return listFile; }
        public List<string[]> getHomologys() { return homologys; }
        public HashSet<string> getFamily() { return family; }
        public double getQValue() { return q_value; }
        public double getProbCutoff() { return prob_cutoff; }
        public int getClusterGap() { return cluster_gap; }
        public int getGapSize() { return gap_size; }
        public List<ElementList> getMirRegions() { return mirRegions; }
        public int getTandemGap() { return tandem_gap; }
        public int getHomologyPoints() { return homologypoints; }
        public string getBlastOut() { return blast_out; }
        public string getBlastTable() { return blast_table; }     
        public string getFormatdb() { return formatdb; }
        public string getBlastall() { return blastall; }
        public List<Cluster> getClusters() { return clusters; }
        public Dictionary<string, ResultSeg> getResultSegs() { return resultsegs; }
        public string getMlcFile() { return mlcFile; }
        public string getMldsFile() { return mldsFile; }
        public string getCodemlInput() { return codemlInput; }
        public string getMsyFile() { return msyFile; }
        public string getCodeml() { return codeml; }
        public string getCodemlCtl() { return codeml_ctl; }    
        public bool getIsLoadMsy() { return isLoadMsy; }
        public bool getIsDetected() { return isDetected; }

        public string getHelpFile() { return help_file; }

        public void getGapSizes(int[] gapsizes)
        {
            int nr_gaps = 0;
            double step = (Math.Log(gap_size) / Math.Log(3) - 1) / 9;
            for (int i = 0; i < 10; i++)
            {
                int value = (int)Math.Round(Math.Pow(3, (1 + (i * step))));
                if (i == 0)
                {
                    gapsizes[nr_gaps] = value;
                    nr_gaps++;
                }
                else if (value != gapsizes[nr_gaps - 1])
                {
                    gapsizes[nr_gaps] = value;
                    nr_gaps++;
                }
            }
            if (nr_gaps < 10) gapsizes[nr_gaps] = -100;		//end of the gaps in the array
        }
  
    
    }
}
