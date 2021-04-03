using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;


namespace MirSyn
{
    public class KsData
    {
        Data data;
        Dictionary<string,string> seqs = new Dictionary<string, string>();
        Dictionary<string,string[]> align_mrna = new Dictionary<string,string[]>();
        Dictionary<string,List<string[]>> ks_result = new Dictionary<string, List<string[]>>();        
        HashSet<string> homoMirs = new HashSet<string>(); 
        Dictionary<string,HashSet<string>> family = new Dictionary<string,HashSet<string>>();

        public KsData(Data data)        
        {
            this.data = data;
            homologys();
        }
        
        void homologys()        
        {
            foreach (Cluster cluster in data.getClusters())
            {                
                homoMirs.Add(cluster.getMirX());
                homoMirs.Add(cluster.getMirY());
            }
        }

        public void runCodeml() 
        {
            Match m;
            Regex regex = new Regex(@"^\S+ +[\d\.]+ +\([\d\.]+ +([\d\.]+)\)",RegexOptions.Multiline); 
            //Regex regex = new Regex(@"^\S+ +([\d\.]+)", RegexOptions.Multiline); 
            foreach (KeyValuePair<string, HashSet<string>> pair in family)
            {
                string species = pair.Key;
                HashSet<string> mirs = pair.Value;
                foreach (Cluster cluster in data.getClusters())
                {
                    string mir_x = cluster.getMirX();
                    string mir_y = cluster.getMirY();
                    if (mirs.Contains(mir_x) && mirs.Contains(mir_y))
                    {
                        string mir_a = cluster.getMirX();
                        string mir_b = cluster.getMirY();
                        List<BaseCluster> baseclusters = cluster.getBaseClusters();
                        foreach (BaseCluster basecluster in baseclusters)
                        {
                            List<HomologyPoint> points = basecluster.getHomologyPoints();
                            List<double> dns = new List<double>();
                            foreach (HomologyPoint point in points)
                            {
                                string x_id = point.getGeneX().getID();
                                string y_id = point.getGeneY().getID();
                                string xy_id = x_id + "_" + y_id;
                                if (align_mrna.ContainsKey(xy_id))
                                {
                               
                                    try
                                    {
                                        StreamWriter sw = new StreamWriter(new FileStream(data.getCodemlInput(), FileMode.Create));
                                        string[] items = align_mrna[xy_id];
                                        sw.Write("2 " + items[0].Length + "\n\n");
                                        sw.Write(x_id + "\n");
                                        sw.Write(items[0] + "\n\n");
                                        sw.Write(y_id + "\n");
                                        sw.Write(items[1] + "\n\n");
                                        sw.Flush();
                                        sw.Close();
                                        ExternalWrapper ew = new ExternalWrapper(data.getCodeml(), data.getCodemlCtl());
                                        ew.runXternal();
                                        StreamReader sr = new StreamReader(new FileStream(data.getMlcFile(), FileMode.Open));
                                        //StreamReader sr = new StreamReader(new FileStream(data.getMldsFile(), FileMode.Open));
                                        string line;
                                        while ((line = sr.ReadLine()) != null)
                                        {
                                            m = regex.Match(line);
                                            if (m.Success)
                                            {
                                                double ds = Double.Parse(m.Groups[1].ToString());
                                                dns.Add(ds);
                                                break;
                                            }
                                        }
                                        sr.Close();
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(e.Message);
                                    }
                                }                                
                            }
                            if (dns.Count == 0) {
                                continue;
                            }
                            double mean = calculateMean(dns);
                            double sd = calculateSD(dns);
                            double min = calculateMin(dns);
                            double max = calculateMax(dns);
                            //string ks = dns.Count + "\t" + mean.ToString("#0.00") + "\t" + sd.ToString("#0.00") + "\t"
                            //            + min.ToString("#0.00") + "\t" + max.ToString("#0.00");
                            string ks = mean.ToString("#0.00");                            
                           
                            string[] arrary = { mir_a, mir_b, ks };
                            if (ks_result.ContainsKey(species))
                            {
                                ks_result[species].Add(arrary);
                            }
                            else 
                            {
                                List<string[]> list = new List<string[]>();
                                list.Add(arrary);
                                ks_result.Add(species, list);
                            }                           
                        }
                    }
                }
            }
        }
      
        public bool loadMsyFile()
        {                         
            XmlDocument doc = new XmlDocument();
            doc.Load(data.getMsyFile());
            
            XmlNodeList top_list = doc.DocumentElement.ChildNodes;
            foreach (XmlElement top in top_list)
            {
                if (top.Name == "family")
                {
                    string species = top.Attributes["species"].Value;                        
                    XmlNodeList gene_list = top.ChildNodes;
                    foreach (XmlElement gene in gene_list)
                    {
                        string mir = gene.Attributes["id"].Value; ;
                        if (family.ContainsKey(species))
                        {
                           family[species].Add(mir);
                        }
                        else
                        {
                           HashSet<string> mirs = new HashSet<string>();
                           mirs.Add(mir);
                           family.Add(species, mirs);
                        } 
                     }
                }
                else if (top.Name == "neighbor")
                {
                    XmlNodeList gene_list = top.ChildNodes;
                    foreach (XmlElement gene in gene_list)
                    {
                        string id = gene.Attributes["id"].Value;
                        XmlNodeList seq_list = gene.ChildNodes;
                        string seq = null;                        
                        foreach (XmlElement seq_ele in seq_list)
                        {
                            if (seq_ele.Name == "seq")
                            {
                                seq = seq_ele.InnerText;
                                if (seq != null && seq.Trim() != "")
                                    seqs[id] = seq;   
                            }
                        }                           
                    }                        
                }
            } 
            return true;
        }

        double calculateMean(List<double> list) 
        {
            double sum = 0;
            double mean;            
            foreach(double el in list)
            {
                sum += el;
            }
            mean = sum / list.Count;
            return mean;
        }

        double calculateSD(List<double> list) 
        {
            double s = -1;
            double sum_x2 = 0;
            double sum_x = 0;
            foreach (double el in list)
            {
                sum_x2 += (el * el);
                sum_x += el;
            }
            int n = list.Count;
            if(n > 1)
            {
                s = Math.Sqrt((sum_x2 - sum_x * sum_x / n) / (n - 1));
            }            
            return s;
        }

        double calculateMin(List<double> list)
        {
            double min = list[0];
            foreach (double el in list)
            {
                if(min > el)
                {
                    min = el;
                }
            }
            return min;        
        }

        double calculateMax(List<double> list)
        {
            double max = list[0];
            foreach (double el in list)
            {
                if (max < el)
                {
                    max = el;
                }
            }
            return max;
        }

        public void parseBlast()
        {
            try
            {               
                StreamReader sr = new StreamReader(new FileStream(data.getBlastOut(), FileMode.Open));
                bool result_start = false;
                bool hit_start = false;
                bool eof = false;
                bool read = true;
                Match m;
                string line = null;
                string queryName = null;

                line = sr.ReadLine();
                if (line == null)
                {
                    MessageBox.Show("The blast output is empty!");
                    Application.Exit();
                }
                // *************** START OF BIG LOOP ****************                
                while (!eof)
                {
                    if (read)
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
                        while (true)
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
                        int len = Int32.Parse(m.Groups[2].Value);
                        double iden = Double.Parse(m.Groups[1].Value) / Double.Parse(m.Groups[2].Value);
                        sr.ReadLine();
                        line = sr.ReadLine();
                        StringBuilder querySeq = new StringBuilder(5000);
                        StringBuilder subjectSeq = new StringBuilder(5000);
                        m = Regex.Match(line, @"Query: +(\d+) +([\w\*-]+) +(\d+)");
                        int queryStart = Int32.Parse(m.Groups[1].Value);
                        int queryEnd = Int32.Parse(m.Groups[3].Value);
                        querySeq.Append(m.Groups[2].Value);
                        sr.ReadLine();
                        line = sr.ReadLine();
                        m = Regex.Match(line, @"Sbjct: +(\d+) +([\w\*-]+) +(\d+)");
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
                            StringBuilder query_sb = new StringBuilder();
                            StringBuilder subject_sb = new StringBuilder();
                            char[] query_pep = querySeq.ToString().ToCharArray();
                            char[] subject_pep = subjectSeq.ToString().ToCharArray();
                            string query_mrna = seqs[queryName];
                            string subject_mrna = seqs[subjectName];
                            int j = 0;
                            int k = 0;
                            for (int i = 0; i < query_pep.Length; i++ )
                            {                               
                                if (query_pep[i] != '-' && subject_pep[i] != '-')
                                {                                    
                                    int q_index = (j + queryStart - 1) * 3;
                                    int s_index = (k + subjectStart - 1) * 3;
                                    System.Diagnostics.Debug.Assert(
                                        q_index <= query_mrna.Length - 3,
                                        "query index in " + queryName + " at position " + q_index + "\t" + query_mrna);
                                    System.Diagnostics.Debug.Assert(
                                        s_index <= subject_mrna.Length - 3,
                                        "subject index in " + subjectName + " at position " + "\t" + s_index + subject_mrna);
                                    query_sb.Append(query_mrna.Substring(q_index, 3));                                    
                                    subject_sb.Append(subject_mrna.Substring(s_index, 3));
                                    j++;
                                    k++;
                                }
                                else if (query_pep[i] != '-' && subject_pep[i] == '-')
                                {
                                    j++;
                                }
                                else if (query_pep[i] == '-' && subject_pep[i] != '-')
                                {
                                    k++;
                                }
                            }
                            string key = queryName + "_" + subjectName;
                            string[] value = { query_sb.ToString(), subject_sb.ToString() };
                            if (!align_mrna.ContainsKey(key)) {
                                align_mrna.Add(key, value);
                            }
                        }
                        hit_start = false;
                        while (true)
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
                            if (Regex.Match(line, @"Query= (\S+)").Success)
                            {
                                result_start = true;
                                break;
                            }                           
                        }
                    }
                }
                // *************** END OF BIG LOOP ****************
                sr.Close();               
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Get
        public List<string> getHomoMirs(){ return homoMirs.ToList();}
        public Dictionary<string, List<string[]>> getKs_result() { return ks_result; }
    }
}
