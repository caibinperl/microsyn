using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MirSyn
{
    public static class Translation
    {
        public static string translate(string id, string cds) 
        {
            Dictionary<string, char> codes = new Dictionary<string, char>();
            codes.Add("ATG", 'M');
            codes.Add("TAA", '*');
            codes.Add("TAG", '*');
            codes.Add("TGA", '*');

            codes.Add("TGG", 'W');

            codes.Add("TTT", 'F');
            codes.Add("TTC", 'F');

            codes.Add("TTA", 'L');
            codes.Add("TTG", 'L');
            codes.Add("CTT", 'L');
            codes.Add("CTC", 'L');
            codes.Add("CTA", 'L');
            codes.Add("CTG", 'L');

            codes.Add("ATT", 'I');
            codes.Add("ATC", 'I');
            codes.Add("ATA", 'I');

            codes.Add("GTT", 'V');
            codes.Add("GTC", 'V');
            codes.Add("GTA", 'V');
            codes.Add("GTG", 'V');

            codes.Add("TCT", 'S');
            codes.Add("TCC", 'S');
            codes.Add("TCA", 'S');
            codes.Add("TCG", 'S');

            codes.Add("CCT", 'P');
            codes.Add("CCC", 'P');
            codes.Add("CCA", 'P');
            codes.Add("CCG", 'P');

            codes.Add("ACT", 'T');
            codes.Add("ACC", 'T');
            codes.Add("ACA", 'T');
            codes.Add("ACG", 'T');

            codes.Add("GCT", 'A');
            codes.Add("GCC", 'A');
            codes.Add("GCA", 'A');
            codes.Add("GCG", 'A');

            codes.Add("TAT", 'Y');
            codes.Add("TAC", 'Y');


            codes.Add("CAT", 'H');
            codes.Add("CAC", 'H');

            codes.Add("CAA", 'Q');
            codes.Add("CAG", 'Q');

            codes.Add("AAT", 'N');
            codes.Add("AAC", 'N');
            codes.Add("AAA", 'K');
            codes.Add("AAG", 'K');

            codes.Add("GAT", 'D');
            codes.Add("GAC", 'D');

            codes.Add("GAA", 'E');
            codes.Add("GAG", 'E');

            codes.Add("TGT", 'C');
            codes.Add("TGC", 'C');

            codes.Add("CGT", 'R');
            codes.Add("CGC", 'R');
            codes.Add("CGA", 'R');
            codes.Add("CGG", 'R');
            codes.Add("AGA", 'R');
            codes.Add("AGG", 'R');

            codes.Add("AGT", 'S');
            codes.Add("AGC", 'S');

            codes.Add("GGT", 'G');
            codes.Add("GGC", 'G');
            codes.Add("GGA", 'G');
            codes.Add("GGG", 'G');            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= cds.Length - 3; i = i + 3 )
            {
                string tri = cds.Substring(i, 3);
                if (tri.Length == 3)
                {
                    if (codes.ContainsKey(tri))
                    {
                        sb.Append(codes[tri].ToString());
                    }
                    else
                    {
                        //sb.Append('X');
                        MessageBox.Show("Check the code in " + id + "at positon: " + i);
                        return null;
                    }                                     
                }
                else 
                {
                    MessageBox.Show("Check the mRNA seq!");   
                }
            }
            return sb.ToString();
        }
    }
}
