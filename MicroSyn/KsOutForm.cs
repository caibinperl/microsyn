using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MirSyn
{
    public partial class KsOutForm : Form
    {
        KsData ks_data;
        StringBuilder text = new StringBuilder();

        public KsOutForm(KsData ks_data)
        {
            this.ks_data = ks_data;
            InitializeComponent();
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
           
            ColumnHeader genomic_x = new ColumnHeader();
            ColumnHeader genomic_y = new ColumnHeader();
            ColumnHeader ks  = new ColumnHeader();
            genomic_x.Text = "Genomic_x";
            genomic_y.Text = "Genomic_y";
            ks.Text = "Ks";
            this.listView1.Columns.Clear();
            this.listView1.Columns.AddRange(new ColumnHeader[] { genomic_x, genomic_y, ks });
            this.listView1.Items.Clear();
            outPut();
        }

        void outPut()         
        {
            text.Append("Ks result\r\n");
            text.Append("Genomic_x\tGenomic_y\tKs\r\n");          
            Dictionary<string, List<string[]>> ks_results = ks_data.getKs_result();
            foreach (KeyValuePair<string, List<string[]>> pair in ks_results)
            {                
                List<string[]> ks_list = pair.Value;
                foreach (string[] ks_array in ks_list)
                {
                    this.listView1.Items.Add(new ListViewItem(ks_array));
                    string mir_x = ks_array[0];
                    string mir_y = ks_array[1];
                    string ks = ks_array[2];                    
                    text.Append(mir_x + "\t" + mir_y + "\t" + ks + "\r\n");
                }
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void saveAsTXTFileMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text file (*.txt;*.TXT)|*.txt;*.TXT|" + "All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog.FileName;
                if (file != null)
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter(new FileStream(file, FileMode.Create));
                        sw.Write(text.ToString());
                        sw.Flush();
                        sw.Close();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }
        }
    }
}
