using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MirSyn
{
    public partial class MsyForm : Form
    {
        
        MsyData msyData = new MsyData();
        
        public MsyForm()
        {
            InitializeComponent();
        }       

        private void geneListButton_Click(object sender, EventArgs e)
        {
            new GeneFamilyForm(msyData).Show();
        }

        private void seqFileButton_Click(object sender, EventArgs e)
        {
            seq0penFileDialog.Multiselect = true;
            if (seq0penFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string[] files = seq0penFileDialog.FileNames;
                if (files != null && files.Length != 0)
                {
                    msyData.setseqFiles(files);                    
                }
                else 
                {
                    MessageBox.Show("Failed to select sequence files!");
                }
                
            }
        }

        private void msyFileButton_Click(object sender, EventArgs e)
        {
            msySaveFileDialog.Filter = "MSY file (*.msy;*.MSY)|*.msy;*.MSY|" + "All files (*.*)|*.*";
            msySaveFileDialog.DefaultExt = "msy";
            msySaveFileDialog.AddExtension = true;
            if (msySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = msySaveFileDialog.FileName;
                if (file != null)
                {
                    msyData.setMsyFile(file);
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (msyData.checkData2()) 
            {
                msyData.readSeqFile();
                msyData.checkRedundancy();
                msyData.writeMsyFile();
                this.Dispose();            
            }           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }        
    }
}
