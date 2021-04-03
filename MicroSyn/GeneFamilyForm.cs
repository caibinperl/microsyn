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
    public partial class GeneFamilyForm : Form
    {

        MsyData msyData;        
        
        public GeneFamilyForm(MsyData msyData)
        {
            this.msyData = msyData;
            InitializeComponent();
        }

        void listFileButton_Click(object sender, EventArgs e)
        {
            listOpenFileDialog.Multiselect = false;
            if (listOpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string file = listOpenFileDialog.FileName;
                if (file != null)
                {
                    msyData.setListFile(file);
                }
                else
                {
                    MessageBox.Show("Failed to select list file!");                
                }
            }
        }

        void okButton_Click(object sender, EventArgs e)
        {
            msyData.setSpecies(speciesTextBox.Text.Trim());
                
            if (msyData.checkData())
            {
                string range = mirRegionLenTextBox.Text.Trim();
                if (range.Length == 0)
                {
                    msyData.setMirRegionLen(50);
                }
                else
                {
                    msyData.setMirRegionLen(Int32.Parse(range));
                }
                msyData.laodFamily();
                msyData.loadMirRegions();
            }
            this.Dispose();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void familyFileBbutton_Click(object sender, EventArgs e)
        {
            familyOpenFileDialog.Multiselect = false;
            if (familyOpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string file = familyOpenFileDialog.FileName;
                if (file != null)
                {
                    msyData.setFamilyFile(file);
                }
                else
                {
                    MessageBox.Show("Failed to select list file!");
                }
            }
        }        
    }
}
