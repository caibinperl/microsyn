using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace MirSyn
{
    public class MsyData
    {
        int mirRegionLen = 50;
        string msyFile;
        string listFile;
        string[] seqFiles;

        string species;
        string familyFile;
       
        //<species,mirs>
        Dictionary<string, HashSet<string>> family = new Dictionary<string, HashSet<string>>();
        Dictionary<string, List<Gene>> mir_genes = new Dictionary<string, List<Gene>>();
        //<mir chr gene1 gene2 ...>, <mir chr gene1 gene2 ...>, ...
        Dictionary<string, List<Gene>> regions = new Dictionary<string, List<Gene>>();
        List<List<string>> mirRegions = new List<List<string>>();

        HashSet<string> neigbors = new HashSet<string>();

        //<id, seq>
        Dictionary<string, string> seqs = new Dictionary<string, string>();
        Dictionary<string, string> homologys = new Dictionary<string, string>();
        
        //<Chr, Genes>
        Dictionary<string, List<Gene>> genes_info = new Dictionary<string, List<Gene>>();

        //Set
        public void setSpecies(string species) { this.species = species; }
        public void setFamilyFile(string familyFile) { this.familyFile = familyFile; }
        public void setListFile(string listFile) { this.listFile = listFile; }
        public void setMsyFile(string msyFile) { this.msyFile = msyFile; }
        public void setMirRegionLen(int len) { this.mirRegionLen = len; }
        public void setseqFiles(string[] seqFiles) { this.seqFiles = seqFiles; }

        //Get
        public Dictionary<string, string> getSeqs() { return seqs; }  //<string, string>
        public Dictionary<string, string> getHomologys() { return homologys; } //<string, string>
        public Dictionary<string, HashSet<string>> getFamily() { return family; }
        public string getListFile() { return listFile; }
        public int getMirRegionLen() { return mirRegionLen; }
        public List<List<string>> getMirRegions() { return mirRegions; }

        //
        public void laodFamily()
        {     
            HashSet<string> mirs = new HashSet<string>();            
            try
            {
                StreamReader sr = new StreamReader(new FileStream(familyFile, FileMode.Open));
                string line;
                string id;
                while ((line = sr.ReadLine()) != null)
                {
                    id = line.Trim();
                    if(id.Length == 0)
                        continue;
                    mirs.Add(id);
                }
                sr.Close();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }

            family.Add(species, mirs);
        }

        public void loadMirRegions()
        {
            genes_info.Clear();
            try
            {
                StreamReader sr = new StreamReader(new FileStream(listFile, FileMode.Open));
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //chr1	Vv01s0010g00020	14353846	14356865	-
                    string[] items = Regex.Split(line.Trim(), "\\t");
                    if (items.Length != 5)
                        continue;
                    string chrName = items[0];
                    string geneName = items[1];
                    string position_s = items[2];
                    string orientation_s = items[4];
                    if (chrName.Length != 0 && geneName.Length != 0 && orientation_s.Length != 0 && position_s.Length != 0)
                    {
                        int orientation = 1;
                        if (orientation_s.Equals("-"))
                        {
                            orientation = 0;
                        }
                        int position = Int32.Parse(position_s);
                        //fill genes_info
                        if (genes_info.ContainsKey(chrName))
                        {
                            List<Gene> genes = genes_info[chrName];
                            genes.Add(new Gene(geneName, chrName, position, orientation));
                        }
                        else
                        {
                            List<Gene> genes = new List<Gene>();
                            genes.Add(new Gene(geneName,chrName, position, orientation));
                            genes_info.Add(chrName, genes);
                        }
                    }
                }
                sr.Close();

                //Sort genes in genes_info acoording to positon in GFF
                foreach (KeyValuePair<string, List<Gene>> pair in genes_info)
                {
                    List<Gene> genes = pair.Value;
                    GenesComparator gc = new GenesComparator();
                    genes.Sort(gc);
                }

                HashSet<string> mirs = new HashSet<string>();
                mirs = family[species];                
                mir_genes[species] = new List<Gene>();
                foreach (KeyValuePair<string, List<Gene>> pair in genes_info)
                {
                    string chr = pair.Key;
                    List<Gene> genes = pair.Value;
                    for (int j = 0; j < genes.Count; j++)
                    {
                        Gene gene = genes[j];
                        if (mirs.Contains(gene.getID()))
                        {
                            mir_genes[species].Add(gene);
                            string mir_id = gene.getID();
                            List<Gene> elements = new List<Gene>();
                            //elements.Add(gene);                            
                            for (int k = j - mirRegionLen; k <= j + mirRegionLen; k++)
                            {
                                if (k >= 0 && k < genes.Count)
                                {
                                    Gene gene2 = genes[k];                                   
                                    elements.Add(gene2);
                                    neigbors.Add(gene2.getID());
                                }
                            }
                            regions[mir_id] =  elements;
                        }
                    }

                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        public void readSeqFile()
        {
            for (int i = 0; i < seqFiles.Length; i++)
            {
                try
                {
                    StreamReader sr = new StreamReader(new FileStream(seqFiles[i], FileMode.Open));
                    int inByte;
                    char ch;                    
                    do
                    {
                        inByte = sr.Read();
                        ch = (char)inByte;
                    } while (inByte != -1 && ch != '>');  // Find '>'
                    while (inByte != -1)
                    {
                        StringBuilder id = new StringBuilder(256);
                        StringBuilder seq = new StringBuilder(10000);
                        //Read id
                        while ((inByte = sr.Read()) != -1 && (ch = (char)inByte) != '\r' && ch != '\n')
                        {
                            id.Append(ch);
                        }
                        //read seq, skip \n
                        while ((inByte = sr.Read()) != -1 && (ch = (char)inByte) != '>')
                        {
                            if (ch != '\n')
                                seq.Append(ch);
                        }
                        string name = id.ToString().Trim();
                        string sequence = seq.ToString().Trim();
                        if (neigbors.Contains(name))
                        {
                            seqs.Add(name, sequence.ToUpper().Replace('U','T'));
                        }
                    }
                    sr.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
               
        public bool checkData()
        {
            if (familyFile == null)
            {
                MessageBox.Show("Have not select family gene file !");
                return false;
            }
            if (species.Trim().ToString().Length == 0)
            {
                MessageBox.Show("Have not input species!");
                return false;
            }
            if (listFile == null)
            {
                MessageBox.Show("Have not select list file!");
                return false;
            }
            return true;
        }

        public bool checkData2()
        {
            if (familyFile == null)
            {
                MessageBox.Show("Do step 1 first !");
                return false;
            }
            if (species == null)
            {
                MessageBox.Show("Do step 1 first !");
                return false;
            }
            if (listFile == null)
            {
                MessageBox.Show("Do step 1 first !");
                return false;
            }
            if (seqFiles == null)
            {
                MessageBox.Show("Do step 2 first !");
                return false;
            }
            if (msyFile == null)
            {
                MessageBox.Show("Do step 3 first!");
                return false;
            }           
            return true;
        }

        public void writeMsyFile()
        {
            XmlDocument doc = new XmlDocument(); 
            XmlNode dec = doc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            doc.AppendChild(dec);

            XmlElement root = doc.CreateElement("msy");
            doc.AppendChild(root);

            XmlElement family;            
            XmlElement gene;
            XmlElement neighbor;

            XmlElement xmlelem;
            XmlText xmltext;

            foreach (KeyValuePair<string, List<Gene>> pair in mir_genes)
            {
                string key = pair.Key;
                List<Gene> value = pair.Value;
                family = doc.CreateElement("family");
                family.SetAttribute("species", key);                    

                foreach (Gene mir in value)
                {
                    gene = doc.CreateElement("gene");                    
                    gene.SetAttribute("id", mir.getID());
                    gene.SetAttribute("chr", mir.getChr());
                    gene.SetAttribute("strand", mir.getOrientation().ToString());
                    gene.SetAttribute("pos", mir.getPosition().ToString());
                    family.AppendChild(gene);
                }
                root.AppendChild(family);
            }

            foreach (KeyValuePair<string, List<Gene>> pair in regions)          
            {
                string mir_id = pair.Key;  

                neighbor = doc.CreateElement("neighbor");
                neighbor.SetAttribute("family_gene",mir_id);
                root.AppendChild(neighbor);
               
                foreach(Gene nbgene in pair.Value)
                {
                    gene = doc.CreateElement("gene");
                    gene.SetAttribute("id", nbgene.getID());
                    gene.SetAttribute("chr", nbgene.getChr());
                    gene.SetAttribute("strand", nbgene.getOrientation().ToString());
                    gene.SetAttribute("pos", nbgene.getPosition().ToString());
 
                    neighbor.AppendChild(gene);

                    xmlelem = doc.CreateElement("seq");
                    if (seqs.ContainsKey(nbgene.getID()))
                        xmltext = doc.CreateTextNode(seqs[nbgene.getID()]);
                    else
                        xmltext = doc.CreateTextNode("");
                    xmlelem.AppendChild(xmltext);
                    gene.AppendChild(xmlelem); 
                }
            }

            try
            {
                doc.Save(msyFile);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void checkRedundancy()
        {
            List<string> check_mir = new List<string>();
            foreach (KeyValuePair<string, List<Gene>> pair in mir_genes)
            {
                string key = pair.Key; //species
                List<Gene> value = pair.Value;
                foreach (Gene mir in value)
                {
                    string mir_id = mir.getID();
                    if (check_mir.Contains(mir_id))
                    {
                        MessageBox.Show(mir_id + "is redundant");
                        Application.Exit();
                    }
                    check_mir.Add(mir_id);                   
                }                
            }

            foreach (KeyValuePair<string, List<Gene>> pair in regions)
            {
                List<string> check_nb = new List<string>();
                string mir_id = pair.Key;
                foreach (Gene nbgene in pair.Value)
                {
                    string nbgene_id = nbgene.getID();
                    if (check_nb.Contains(nbgene_id))
                    {
                        MessageBox.Show(nbgene_id + "is redundant");
                        Application.Exit();
                    }
                    check_nb.Add(nbgene_id);   
                }
            }
        }
    
    }



    class GenesComparator: IComparer<Gene>
    {
        public int Compare(Gene g1, Gene g2)
        {
            return g1.getPosition() - g2.getPosition();
        }
    }

}


