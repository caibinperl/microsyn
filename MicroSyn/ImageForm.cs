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
    public partial class ImageForm : Form
    {
       
        int width;
        int height;
        int size_w;
        int size_h;
        int radiu;
        double interval;
        double node_width;

        Dictionary<string, ResultSeg> resultsegs;
       // List<string> mirs = new List<string>();
        List<Text> texts = new List<Text>();

        ImageData data;

        Bitmap bmp;
        Graphics graphics;
        Font font;
        
        public ImageForm(ImageData data)
        {       
            this.data = data;
            this.width = this.data.getWidth();
            this.height = this.data.getHeight();
            this.radiu = this.data.getRadiu();
            this.interval = this.data.getInterval();
            this.node_width = this.data.getNodeWidth();
            size_w = this.width + 8;
            size_h = this.height + 8;
            this.resultsegs = this.data.getResultSegs();
            this.texts = this.data.getTexts();
            InitializeComponent();
            this.Size = new Size(size_w, size_h);
            this.font = this.data.getFont();
            this.draw();
        }

        void draw()
        {
           
            if (bmp != null)
            {
                bmp.Dispose();
            }
            bmp = new Bitmap(this.width, this.height);
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


        private void circlePaint()
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
                    foreach (Element el in element.getHomologyElements())
                    {
                        if (el != null)
                        {
                            Pen pen = new Pen(Color.Gray, 0.5F);
                            graphics.DrawLine(pen, new Point((int)element.getCircleX(), (int)element.getCircleY()),
                                        new Point((int)el.getCircleX(), (int)el.getCircleY()));
                        }
                    }
                }
            }
            //draw  text              
            for (int i = 0; i < texts.Count; i++)
            {                
                SolidBrush brush = new SolidBrush(Color.Blue);
                graphics.DrawString(texts[i].getMirID(), font, brush, texts[i].getX(), texts[i].getY());
            }
        }

        private void circle2Paint()
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
                    foreach (Element el in element.getHomologyElements())
                    {
                        if (el != null)
                        {
                            Pen pen = new Pen(Color.Gray, 0.5F);
                            graphics.DrawLine(pen, new Point((int)element.getCircleX(), (int)element.getCircleY()),
                                        new Point((int)el.getCircleX(), (int)el.getCircleY()));
                        }
                    }

                }
            }
            //draw  text              
            for (int i = 0; i < texts.Count; i++)
            {
                SolidBrush brush = new SolidBrush(Color.Blue);
                graphics.DrawString(texts[i].getMirID(), font, brush, texts[i].getX(), texts[i].getY());
            }
        }
    }
}
