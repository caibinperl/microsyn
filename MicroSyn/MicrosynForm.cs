using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace MirSyn
{
    public partial class MicroSynForm : Form
    {
        Data data = new Data();
        KsData ks_data;
        bool ifAlive = false;
        bool ifAbort = false;

        public MicroSynForm()
        {
            InitializeComponent();           
            data.setURL();
            
            ks_data = new KsData(data);
        }

        protected override void OnClosed(EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            System.Environment.Exit(0);
        }

        private void msyMenuItem_Click(object sender, EventArgs e)
        {
            new MsyForm().Show();
        }        

        private void runMenuItem_Click(object sender, EventArgs e)
        {
            if (data.getIsLoadMsy())
            {
                new RunForm(data).Show();                
            }
            else
            {
                MessageBox.Show("Please load the MSY file first!");
            }
            runMenuItem.Enabled = false;
            toolStripButton3.Enabled = false;

        }

        private void ksMenuItem_Click(object sender, EventArgs e)
        {
            if(data.getIsDetected())
            {
                this.statusLabel.Text = "Status: detecting Ks ...... !";               

                Thread ksThread = new Thread(new ThreadStart(ksFunc));
                ksThread.Start();

                while (true)
                {
                    ifAlive = ksThread.IsAlive;
                    System.Windows.Forms.Application.DoEvents();

                    if (!ifAlive)
                    {
                        break;
                    }
                    if (ifAbort)
                    {
                        ksThread.Interrupt();
                        ksThread.Abort();
                        break;
                    }
                    Thread.Sleep(300);
                }
                this.statusLabel.Text = "Status: detecting Ks is done!";

                new KsOutForm(ks_data).Show();               
                clearTrashFiles();                                
            }
            else
            {
                MessageBox.Show("Please detect microsyn first!");            
            }            
        }

        private void ksFunc()
        {            
            ks_data.loadMsyFile();
            ks_data.parseBlast();
            ks_data.runCodeml();
        } 

        void clearTrashFiles()
        {
            if (File.Exists("2NG.dN"))
            {
                File.Delete("2NG.dN");
            }

            if (File.Exists("2NG.dS"))
            {
                File.Delete("2NG.dS");
            }

            if (File.Exists("2NG.t"))
            {
                File.Delete("2NG.t");
            }

            if (File.Exists("rst"))
            {
                File.Delete("rst");
            }

            if (File.Exists("rst1"))
            {
                File.Delete("rst1");
            }

            if (File.Exists("rub"))
            {
                File.Delete("rub");
            }
        }



        private void dataFileMenuItem_Click(object sender, EventArgs e)
        {
            msyOpenFileDialog.Filter = "MSY file (*.msy;*.MSY)|*.msy;*.MSY|" + "All files (*.*)|*.*";
            msyOpenFileDialog.Multiselect = false;
            if (msyOpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string file = msyOpenFileDialog.FileName;
                if (file != null)
                {
                    data.setMsyFile(file);
                    if (data.loadMsyFile())
                    {
                        statusLabel.Text = "MSF file: " + file;
                        data.setIsLoadMsy(true);
                        closeDataMenuItem.Enabled = true;
                        closeDataButton.Enabled = true;
                        runMenuItem.Enabled = true;
                        toolStripButton3.Enabled = true;
                    }                    
                }
                else
                {
                    MessageBox.Show("Failed to select list file!");
                }
            }
        }

        private void closeDataMenuItem_Click(object sender, EventArgs e)
        {
            data.clearMsy();
            closeDataMenuItem.Enabled = false;
            closeDataButton.Enabled = false;
        }

        private void MicroSynForm_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataFileButton_Click(object sender, EventArgs e)
        {
            msyOpenFileDialog.Filter = "MSY file (*.msy;*.MSY)|*.msy;*.MSY|" + "All files (*.*)|*.*";
            msyOpenFileDialog.Multiselect = false;
            if (msyOpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string file = msyOpenFileDialog.FileName;
                if (file != null)
                {
                    data.setMsyFile(file);
                    if (data.loadMsyFile())
                    {
                        statusLabel.Text = "MSF file: " + file;
                        data.setIsLoadMsy(true);
                        closeDataMenuItem.Enabled = true;
                        closeDataButton.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to select list file!");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            System.Environment.Exit(0);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (data.getIsLoadMsy())
            {
                new RunForm(data).Show();
            }
            else
            {
                MessageBox.Show("Please load the MSY file first!");
            }
        }

        private void closeDataButton_Click(object sender, EventArgs e)
        {
            data.clearMsy();
            closeDataMenuItem.Enabled = false;
            closeDataButton.Enabled = false;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkLabel.LinkVisited = true;
                //Call the Process.Start method to open the default browser 
                //with a URL:
                System.Diagnostics.Process.Start("http://fcsb.njau.edu.cn/microsyn");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }

        }

        private void generateMysMenuItem_Click(object sender, EventArgs e)
        {
            new MsyForm().Show();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void generateMysStripButton_Click(object sender, EventArgs e)
        {
            new MsyForm().Show();
        }

        private void kstoolStripButton_Click(object sender, EventArgs e)
        {
            if (data.getIsDetected())
            {
                this.statusLabel.Text = "Status: detecting Ks ...... !";
                KsData ks_data = new KsData(data);
                ks_data.loadMsyFile();
                ks_data.parseBlast();
                ks_data.runCodeml();
                this.statusLabel.Text = "Status: detecting Ks is done!";
                new KsOutForm(ks_data).Show();
                clearTrashFiles();
            }
            else
            {
                MessageBox.Show("Please detect microsyn first!");
            }            
        }

        private void ksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(data.getHelpFile());
        }

        private void viewSyntenyMenuItem_Click(object sender, EventArgs e)
        {

        }         
    }
}
