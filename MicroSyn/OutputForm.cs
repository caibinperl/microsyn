using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;

namespace MirSyn
{
    public partial class OutputForm : Form
    {
        Data data;

        HashSet<string> mir_ids;
        List<ElementList> lists;
        Dictionary<string, ResultSeg> resultsegs;
        List<Cluster> clusters;

        List<string[]> mirss = new List<string[]>();       
        List<string[]> neighbors = new List<string[]>();
        List<string[]> syntenys = new List<string[]>();
        List<string[]> homologys = new List<string[]>();
        StringBuilder text = new StringBuilder();

        ImageData image_data;
            
        public OutputForm(Data data)
        {           
            this.data = data;
            mir_ids = data.getFamily();
            lists = data.getMirRegions();
            resultsegs = data.getResultSegs();
            clusters = data.getClusters();

            InitializeComponent();
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;

            image_data = new ImageData(this.data);
            //printResult();
        }
                
        private void OutputForm_Load(object sender, EventArgs e)
        {
            text.Append("------ Input data -------\r\n");
            text.Append("\r\n\r\n");
            text.Append("Genes in faimily\r\n");

           
            foreach (string mir_id in mir_ids)
            {
                for (int i = 0; i < lists.Count; i++)
                {
                    Gene gene = lists[i].getGeneByID(mir_id);
                    if (gene != null)
                    {                        
                        string list_name = lists[i].getListName();
                        string[] arrayStr = list_name.Split('_');
                        list_name = arrayStr[0];
                        mirss.Add(new string[] { gene.getID(), gene.getChr(), gene.getPosition().ToString() });
                        text.Append(gene.getID() + "\t" + gene.getChr() + "\t" + gene.getPosition().ToString() + "\r\n");
                    }
                }
            }


            text.Append("\r\n\r\n");
            text.Append("Genes in faimily\tNeighboring gene\tPosition\r\n");
            
            foreach (KeyValuePair<string, ResultSeg> pair in resultsegs)
            {
                string mir_id = pair.Key;
                ElementList list = resultsegs[mir_id].getSeg();
                List<Element> els = list.getRemappedElements();
                
                
                //StringBuilder sb = new StringBuilder();
                for (int i = 0; i < els.Count; i++)
                {
                    string[] neighbor = new string[3];
                    neighbor[0] = mir_id;
                    neighbor[1] = els[i].getGene().getID();
                    neighbor[2] = els[i].getGene().getPosition().ToString();
                    neighbors.Add(neighbor);
                    text.Append(neighbor[0] + "\t" + neighbor[1] + "\t" + neighbor[2] + "\r\n");
                } 
            }

            text.Append("\r\n\r\n");
            text.Append("------ Synteny data -------\r\n");
            text.Append("\r\n");
            text.Append("Segment1\tSegment2\tNumber of homology\tProbability\r\n");
                       
            foreach (Cluster cluster in clusters)
            {
                string[] synteny = new string[4];
                synteny[0] = cluster.getMirX();
                synteny[1] = cluster.getMirY();
                synteny[2] = cluster.getCountHomologyPoints().ToString();
                synteny[3] = cluster.getProbability().ToString();
                syntenys.Add(synteny);
                text.Append(synteny[0] + "\t" + synteny[1] + "\t" + synteny[2] + "\t" + synteny[3] + "\r\n");
            }

            text.Append("\r\n\r\n");
            text.Append("Pair of Segments\tHomoloy genes\r\n");
            foreach (Cluster cluster in data.getClusters())
            {    
                List<BaseCluster> baseclusters = cluster.getBaseClusters();
                //StringBuilder sb = new StringBuilder();
                foreach (BaseCluster basecluster in baseclusters)
                {
                    List<HomologyPoint> points = basecluster.getHomologyPoints();
                    foreach (HomologyPoint point in points)
                    {
                        string[] homology = new string[2];
                        homology[0] = cluster.getMirX() + " - " + cluster.getMirY();         
                        homology[1] = point.getGeneX().getID() + "/" + point.getGeneY().getID();
                        homologys.Add(homology);
                        text.Append(homology[0] + "\t" + homology[1] + "\r\n");
                    }
                }               
                
            }

        }

        private void loadFamily()
        {
            ColumnHeader chr1 = new ColumnHeader();
            ColumnHeader chr2 = new ColumnHeader();
            ColumnHeader chr3 = new ColumnHeader();
            chr1.Text = "Gene in faimily";
            chr2.Text = "Chr";
            chr3.Text = "Position";

            this.listView1.Columns.Clear();
            this.listView1.Columns.AddRange(new ColumnHeader[]{chr1, chr2, chr3});
            this.listView1.Items.Clear();
            for (int i = 0; i < mirss.Count; i++)
            {
                 this.listView1.Items.Add(new ListViewItem(mirss[i]));
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void loadNeighbor()
        {
            
            ColumnHeader chr1 = new ColumnHeader();
            ColumnHeader chr2 = new ColumnHeader();
            ColumnHeader chr3 = new ColumnHeader();
            chr1.Text = "Gene in faimily";
            chr2.Text = "Neighboring gene";
            chr3.Text = "Position";

            this.listView1.Columns.Clear();
            this.listView1.Columns.AddRange(new ColumnHeader[]{chr1, chr2, chr3});           
            this.listView1.Items.Clear();
            
            foreach(string[] items in neighbors)
            {
                this.listView1.Items.Add(new ListViewItem(items));
            }          
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void loadSynteny()
        {
            //Region1	Region2	Count of homology	Probability
            ColumnHeader chr1 = new ColumnHeader();
            ColumnHeader chr2 = new ColumnHeader();
            ColumnHeader chr3 = new ColumnHeader();
            ColumnHeader chr4 = new ColumnHeader();
          
            chr1.Text = "Segment1";
            chr2.Text = "Segment2";
            chr3.Text = "Number of homology";
            chr4.Text = "Expected value";
        
            this.listView1.Columns.Clear();
            this.listView1.Columns.AddRange(new ColumnHeader[] { chr1, chr2, chr3, chr4 });
            this.listView1.Items.Clear();

            foreach (string[] items in syntenys)
            {
                this.listView1.Items.Add(new ListViewItem(items));
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void loadHomology()
        {
            ColumnHeader chr1 = new ColumnHeader();
            ColumnHeader chr2 = new ColumnHeader();
            chr1.Text = "Pair of Segments";
            chr2.Text = "Homoloy genes";
            this.listView1.Columns.Clear();
            this.listView1.Columns.AddRange(new ColumnHeader[] { chr1, chr2 });
            this.listView1.Items.Clear();

            foreach (string[] items in homologys)
            {
                this.listView1.Items.Add(new ListViewItem(items));
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Text = e.Node.Text;
            switch(e.Node.Text)
            {
                case "Genes in faimily":
                    this.loadFamily();
                    break;
                case "Neighboring genes":
                    loadNeighbor();
                    break;
                case "Synteny":
                    loadSynteny();
                    break;
                case "Homologys":
                    loadHomology();
                    break;                    
            }

        }

        private void displayMenuItem_Click(object sender, EventArgs e)
        {
            //new CircleImageForm(data).Show();  
            this.image_data.setCircleData();
            new ImageForm(image_data).Show();
            this.saveEmfMenuItem.Visible = true;
        }

        private void optionsMenuItem_Click(object sender, EventArgs e)
        {
            new OptionForm(image_data).Show();
            this.saveEmfMenuItem.Visible = true;
        }

        private void fontsMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.fontDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.image_data.setFont(this.fontDialog.Font);
                this.image_data.setCircleData();
                new ImageForm(this.image_data).Show();
                this.Dispose();
            }
        }

        private void saveEmfMenuItem_Click(object sender, EventArgs e)
        {
            imageSaveFileDialog.Filter = "EMF file (*.emf;*.EMF)|*.emf;*.EMF";
            imageSaveFileDialog.DefaultExt = "emf";
            imageSaveFileDialog.AddExtension = true;
            if (imageSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = imageSaveFileDialog.FileName;
                if (file != null)
                {
                    //bmp.Save(file, System.Drawing.Imaging.ImageFormat.Emf);

                    Bitmap bitmap = new Bitmap(image_data.getWidth(), image_data.getHeight());
                    Graphics gs = Graphics.FromImage(bitmap);
                    Metafile mf = new Metafile(file, gs.GetHdc());
                    Graphics g = Graphics.FromImage(mf);
                    drawemf(g);
                    g.Save();
                    g.Dispose();
                    mf.Dispose();
                }
            }
        }

        private void drawemf(Graphics g)
        {
            //draw               
            foreach (KeyValuePair<string, ResultSeg> pair in image_data.getResultSegs())
            {
                ElementList list = pair.Value.getSeg();
                List<Element> elements = list.getRemappedElements();
                foreach (Element element in elements)
                {

                    if (element.getGene().IsMirRna())
                    {
                        SolidBrush brush = new SolidBrush(Color.Black);
                        Pen pen = new Pen(Color.Black, 0.5F);
                        g.DrawPolygon(pen, element.getPolygon());
                        g.FillPolygon(brush, element.getPolygon());
                    }
                    else
                    {

                        SolidBrush brush = new SolidBrush(list.getColor());
                        Pen pen = new Pen(list.getColor(), 0.5F);
                        g.DrawPolygon(pen, element.getPolygon());
                        g.FillPolygon(brush, element.getPolygon());
                    }
                    foreach (Element el in element.getHomologyElements())
                    {
                        if (el != null)
                        {
                            Pen pen = new Pen(Color.Gray, 0.5F);
                            g.DrawLine(pen, new Point((int)element.getCircleX(), (int)element.getCircleY()),
                                        new Point((int)el.getCircleX(), (int)el.getCircleY()));
                        }
                    }

                }
            }
            //draw  text   
            List<Text> texts = image_data.getTexts();
            for (int i = 0; i < texts.Count; i++)
            {
                Font font = new Font("Times New Roman", 8);
                SolidBrush brush = new SolidBrush(Color.Blue);
                g.DrawString(texts[i].getMirID(), font, brush, texts[i].getX(), texts[i].getY());
            }
        }

        private void saveAsTXTMenuItem_Click(object sender, EventArgs e)
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void alignMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text file (*.txt;*.TXT)|*.txt;*.TXT|" + "All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog.FileName;
                if (file != null)
                {
                    MultiAligner align = new MultiAligner(data);
                    align.align();
                    align.outputAlign(file);
                    MessageBox.Show("Alignement is done!");
                }
            }
        

        }    
    }
}
