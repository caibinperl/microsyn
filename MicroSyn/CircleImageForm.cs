using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MirSyn
{
    public partial class CircleImageForm : Form
    {
        int width = 950;
        int height = 700;
        int radiu = 250;
        double interval = 5;
        double node_width = 20;
        Dictionary<string, ResultSeg> resultsegs;
        List<Cluster> clusters;
        List<string> mirs = new List<string>();
        List<Text> texts = new List<Text>();
        Data data;
        Bitmap bmp;
        Graphics graphics;

        // Creates new form circleImageFrame
        public CircleImageForm(Data data)
        {
            this.data = data;
            this.clusters = data.getClusters();
            this.resultsegs = data.getResultSegs();
            InitializeComponent();
            if (bmp != null)
            {
                bmp.Dispose();
            }
            sortMirRegions();
            setColorCircle();
            setCircleData();
            drawCircleImage();
        }

        private void setColorCircle()
        {
            int i = 6;
            foreach(KeyValuePair<string, ResultSeg> pair in resultsegs)
            {
                ElementList list = pair.Value.getSeg();
                Random random = new Random(i);
                int r = random.Next(255);
                int g = random.Next(255);
                int b = random.Next(255);
                //MessageBox.Show(r.ToString() + "\t" + g.ToString() + "\t" + b.ToString() );
                list.setColor(r, g, b);
                i = i + 100;
            }
        }

        private void imageOutMenuItem_Click(object sender, EventArgs e)
        {
            imageSaveFileDialog.Filter = "PNG file (*.png;*.PNG)|*.png;*.PNG|" + "All files (*.*)|*.*";
            imageSaveFileDialog.DefaultExt = "png";
            imageSaveFileDialog.AddExtension = true;
            if(imageSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = imageSaveFileDialog.FileName;
                if (file != null)
                {                    
                    //bmp.Save(file, System.Drawing.Imaging.ImageFormat.Emf);
                   
                        Bitmap bitmap = new Bitmap(width, height);
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

        
        private void sortMirRegions()
        {
            for(int i = 0; i < clusters.Count; i++)
            {
                Cluster cluster = clusters[i];
                if(!mirs.Contains(cluster.getMirX())) 
                {
                    mirs.Add(cluster.getMirX());            
                }            
                if(!mirs.Contains(cluster.getMirY()))
                {
                    mirs.Add(cluster.getMirY());
                }            
            }
        }
              
        private void setCircleData()
        {
            int gene_num = getGeneNum();
            if(gene_num != 0)
            {
                double degree = 0;
                double degree_gene = (360 - interval*resultsegs.Count)/gene_num;               
                foreach(string mir_id in mirs)
                {               
                    ElementList list = resultsegs[mir_id].getSeg();
                    List<Element> elements = list.getRemappedElements();                   
                    foreach(Element element in elements)
                    {                        
                                             
                            int x1 = (int)(width/2 + (radiu - node_width/2)*Math.Cos((degree + degree_gene/2)/180*Math.PI));
                            int x2 =  (int)(width/2 + (radiu + node_width/2)*Math.Cos((degree + degree_gene/2)/180*Math.PI));
                            int x3;
                            int y1 = (int)(height/2 - (radiu - node_width/2)*Math.Sin((degree + degree_gene/2)/180*Math.PI));
                            int y2 = (int)(height/2 - (radiu + node_width/2)*Math.Sin((degree + degree_gene/2)/180*Math.PI));
                            int y3;
                            if(element.getOrientation() == 1)
                            {
                                x3 = (int)(width/2 + radiu*Math.Cos((degree + degree_gene/2 + degree_gene/4)/180*Math.PI));
                                y3 = (int)(height/2 - radiu*Math.Sin((degree + degree_gene/2 + degree_gene/4)/180*Math.PI));
                            }
                            else
                            {
                                x3 = (int)(width/2 + radiu*Math.Cos((degree + degree_gene/2 - degree_gene/4)/180*Math.PI));
                                y3 = (int)(height/2 - radiu*Math.Sin((degree + degree_gene/2 - degree_gene/4)/180*Math.PI));
                            }
                            int[] x = {x1, x2, x3};
                            int[] y = {y1, y2, y3};
                            element.setPolygon(x, y);
                            element.setCircleX((double)width/2 + radiu*Math.Cos((degree + degree_gene/2)/180*Math.PI));
                            element.setCircleY((double)height/2 - radiu*Math.Sin((degree + degree_gene/2)/180*Math.PI));
                        
                            if(element.getGene().getID().Equals(mir_id))
                            {
                                if(degree >= 0 && degree < 90)
                                {
                                    texts.Add(new Text(mir_id, x2 + 5, y2 - 25));
                                }
                                else if(degree >= 90 && degree < 180)
                                {
                                    texts.Add(new Text(mir_id, x2 - 120, y2 - 25));
                                }
                                else if(degree >= 180 && degree < 270)
                                {
                                    texts.Add(new Text(mir_id, x2 - 120, y2 + 10));
                                } 
                                else 
                                {
                                    texts.Add(new Text(mir_id, x2 + 5, y2 + 10));
                                }
                            }
                            degree += degree_gene;
                       
                    }
                    degree += interval;
                }
            } 
            else
            {
                MessageBox.Show("The list is empty");
            }
        }

        private void drawemf(Graphics g)
        {
            //draw               
            foreach (KeyValuePair<string, ResultSeg> pair in resultsegs)
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
            for (int i = 0; i < texts.Count; i++)
            {
                Font font = new Font("Times New Roman", 8);
                SolidBrush brush = new SolidBrush(Color.Blue);
                g.DrawString(texts[i].getMirID(), font, brush, texts[i].getX(), texts[i].getY());
            }
        }


        private void circlePaint(){              
            //draw               
            foreach(KeyValuePair<string, ResultSeg> pair in resultsegs)
            {
                ElementList list = pair.Value.getSeg();
                List<Element> elements = list.getRemappedElements();                   
                foreach(Element element in elements)
                {                                     
                   
                        if(element.getGene().IsMirRna())
                        {
                            SolidBrush brush = new SolidBrush(Color.Black);
                            Pen pen = new Pen(Color.Black, 0.5F);
                            graphics.DrawPolygon(pen, element.getPolygon());                           
                            graphics.FillPolygon(brush, element.getPolygon());
                        }
                        else
                        {
                           
                            SolidBrush brush = new SolidBrush(list.getColor());
                            Pen pen = new Pen(list.getColor(), 0.5F);
                            graphics.DrawPolygon(pen, element.getPolygon());
                            graphics.FillPolygon(brush, element.getPolygon()); 
                        } 
                        foreach(Element el in element.getHomologyElements())
                        { 
                            if(el != null)
                            {
                                Pen pen = new Pen(Color.Gray, 0.5F);
                                graphics.DrawLine(pen, new Point((int)element.getCircleX(), (int)element.getCircleY()),
                                            new Point((int)el.getCircleX(), (int)el.getCircleY()));
                            }
                        }
                 
                }
            }
            //draw  text              
            for(int i = 0; i < texts.Count; i++)
            {
                Font font = new Font("Times New Roman", 8);
                SolidBrush brush = new SolidBrush(Color.Blue);     
                graphics.DrawString(texts[i].getMirID(), font, brush, texts[i].getX(), texts[i].getY());
            }
        }

        int getGeneNum()
        {
            int gene_num = 0;            
            foreach (KeyValuePair<string, ResultSeg> pair in resultsegs)
            {
                ElementList list = pair.Value.getSeg();
                List<Element> elements = list.getRemappedElements();
                foreach (Element element in elements)
                {
                    gene_num++;
                }
            }
            return gene_num;
        }

         void drawCircleImage() 
         {
             bmp = new Bitmap(width, height);
             //Graphics gs = Graphics.FromImage(bmp);
             //Metafile mf = new Metafile("", gs.GetHdc());
             //graphics = Graphics.FromImage(mf);
             graphics = Graphics.FromImage(bmp);
             //Metafile mf = new Metafile(filePath, gs.GetHdc());
             circlePaint();             
             circlePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
             circlePictureBox.Image = (Image)bmp;
             graphics.Dispose(); 
         }
    }
}
