using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MirSyn
{
    public partial class RunForm : Form
    {       
        Data data;
        bool ifAlive = false;
        bool ifAbort = false;

        public RunForm(Data data)
        {         
            this.data = data;
           
            InitializeComponent();
        }

        private void runForm_Load(object sender, EventArgs e)
        {
           
        }

        private void runButton_Click(object sender, EventArgs e)
        {
                       
            this.statusLabel.Text = "Status: detecting synteny ...... !";
            data.setGapSize(Int32.Parse(gapSizeTextBox.Text.ToString()));
            data.setHomologyPoints(Int32.Parse(homologyPointsTextBox.Text.ToString()));
            data.setProbCutoff(Double.Parse(probCutoffTextBox.Text.ToString()));
            data.setTandemGap(Int32.Parse(tandemGapTextBox.Text.ToString()));
            if (data.checkData())
            {

                Thread setupThread = new Thread(new ThreadStart(setupFunc));
                setupThread.Start();
                while (true)
                {
                    ifAlive = setupThread.IsAlive;
                    System.Windows.Forms.Application.DoEvents();
                    
                    if (!ifAlive)
                    {                                            
                        break;
                    }
                    if(ifAbort)
                    {
                        setupThread.Interrupt();
                        setupThread.Abort();
                        break;
                    }
                    Thread.Sleep(300);
                }
               
                List<Cluster> clusters = data.getClusters();
                if (clusters.Count != 0)
                {
                    new OutputForm(data).Show();
                    data.setIsDetected(true);
                    //new CircleImageForm(data).Show();
                }
                else
                {
                    MessageBox.Show("No cluster found!");
                }
            }
            this.Dispose();          
        }        

        private void setupFunc()
        {
            //Form runing = new runingForm("Status: detecting synteny ...... !");
            //runing.Show();
            Setup setup = new Setup(data);
            setup.blast();
            setup.parseBlast();
            setup.mapGenes();
            setup.remapTandems();
            setup.Detection();
            //runing.Dispose();
            MessageBox.Show("Compeleted !");
            
        } 

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {

        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            ifAbort = true;
        }
    }
}
