using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MirSyn
{
    public class ImageData
   {
        int radius = 250;
        int width;
        int height;
        
        double interval = 5;
        double node_height = 20;
        double node_width = 0.5;

        Data data;

        Dictionary<string, ResultSeg> resultsegs;
        List<Cluster> clusters;
        List<string> mirs = new List<string>();
        List<Text> texts = new List<Text>();
       
        
        Graphics g;
        Font font;

        public ImageData(Data data)         
        {
            this.width = this.radius*2 + 450;
            this.height = this.radius*2 + 200;
            this.data = data;
            this.clusters = this.data.getClusters();
            this.resultsegs = data.getResultSegs();

            font = new Font("Times New Roman", 8);
            Bitmap bp = new Bitmap(100, 100);
            g = Graphics.FromImage(bp);
            
            sortMirRegions();
            setColorCircle();
            //setCircleData();        
        }
        //Set
        public void setFont(Font font) { this.font = font; }
        //public void setHeight(int height) {this.height = height; }
        public void setRadius(int radius) 
        {
            this.radius = radius;
            this.width = this.radius * 2 + 450;
            this.height = this.radius * 2 + 200;
        }
        public void setInterval(int interval) { this.interval = interval; }
        public void setNodeHeight(int node_height) { this.node_height = node_height; }
        //Get
        public int getWidth() { return width; }
        public int getHeight() { return height; }
        public int getRadiu() { return radius; }
        public double getInterval() { return interval; }
        public double getNodeWidth() { return node_height; }
        public Dictionary<string, ResultSeg> getResultSegs() { return resultsegs; }
        public List<Text> getTexts() { return texts; }

        public Font getFont() { return this.font; }


        private void sortMirRegions()
        {
            for (int i = 0; i < clusters.Count; i++)
            {
                Cluster cluster = clusters[i];
                if (!mirs.Contains(cluster.getMirX()))
                {
                    mirs.Add(cluster.getMirX());
                }
                if (!mirs.Contains(cluster.getMirY()))
                {
                    mirs.Add(cluster.getMirY());
                }
            }
        }

        private void setColorCircle()
        {            
            int i = 6;
            foreach (KeyValuePair<string, ResultSeg> pair in resultsegs)
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

        /*
        public void setCircleData()
        {
            texts.Clear();
            int gene_num = getGeneNum();
            if (gene_num != 0)
            {
                double degree = 0;
                double degree_gene = (360 - interval * resultsegs.Count) / gene_num;
                foreach (string mir_id in mirs)
                {
                    SizeF sizef = g.MeasureString(mir_id, font);

                    ElementList list = resultsegs[mir_id].getSeg();
                    List<Element> elements = list.getRemappedElements();
                    foreach (Element element in elements)
                    {                      
                        int x1 = (int)(width / 2 + (radius - node_height / 2) * Math.Cos((degree + degree_gene / 2) / 180 * Math.PI));
                        int x2 = (int)(width / 2 + (radius + node_height / 2) * Math.Cos((degree + degree_gene / 2) / 180 * Math.PI));
                        int x3;
                        int y1 = (int)(height / 2 - (radius - node_height / 2) * Math.Sin((degree + degree_gene / 2) / 180 * Math.PI));
                        int y2 = (int)(height / 2 - (radius + node_height / 2) * Math.Sin((degree + degree_gene / 2) / 180 * Math.PI));
                        int y3;
                        if (element.getOrientation() == 1)
                        {
                            x3 = (int)(width / 2 + radius * Math.Cos((degree + degree_gene / 2 + degree_gene / 4) / 180 * Math.PI));
                            y3 = (int)(height / 2 - radius * Math.Sin((degree + degree_gene / 2 + degree_gene / 4) / 180 * Math.PI));
                        }
                        else
                        {
                            x3 = (int)(width / 2 + radius * Math.Cos((degree + degree_gene / 2 - degree_gene / 4) / 180 * Math.PI));
                            y3 = (int)(height / 2 - radius * Math.Sin((degree + degree_gene / 2 - degree_gene / 4) / 180 * Math.PI));
                        }
                        int[] x = { x1, x2, x3 };
                        int[] y = { y1, y2, y3 };
                        element.setPolygon(x, y);
                        element.setCircleX((double)width / 2 + radius * Math.Cos((degree + degree_gene / 2) / 180 * Math.PI));
                        element.setCircleY((double)height / 2 - radius * Math.Sin((degree + degree_gene / 2) / 180 * Math.PI));

                        if (element.getGene().getID().Equals(mir_id))
                        {
                            
                            if (degree >= 0 && degree < 90)
                            {
                               //texts.Add(new Text(mir_id, x2 + 5, y2 - 25));
                                texts.Add(new Text(mir_id, x2, y2));
                            }
                            else if (degree >= 90 && degree < 180)
                            {
                                //texts.Add(new Text(mir_id, x2 - 120, y2 - 25));

                                texts.Add(new Text(mir_id, x2 - (int)sizef.Width, y2));
                            }
                            else if (degree >= 180 && degree < 270)
                            {
                                //texts.Add(new Text(mir_id, x2 - 120, y2 + 10));
                                texts.Add(new Text(mir_id, x2 - (int)sizef.Width, y2));
                            }
                            else
                            {
                                //texts.Add(new Text(mir_id, x2 + 5, y2 + 10));
                                texts.Add(new Text(mir_id, x2, y2));
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
        */

        public void setCircleData()
        {
            texts.Clear();
            int gene_num = getGeneNum();
            if (gene_num != 0)
            {
                if (mirs.Count == 2)
                {                   
                    int j = 0;
                    int left_x = (width - radius * 2) / 2;
                    int top_y = (height - radius * 2) / 2;
                   
                    string mir_id1 = mirs[0];
                    string mir_id2 = mirs[1];
                    ElementList list1 = resultsegs[mir_id1].getSeg();
                    ElementList list2 = resultsegs[mir_id2].getSeg();

                    int len = list1.getRemappedElementsLength();
                    if (len < list2.getRemappedElementsLength())
                        len = list2.getRemappedElementsLength();
                   
                   
                    double gene_width = radius * 2  / len;
                    int gene_height = (int)node_height;
                    int tri_width = (int)(gene_width * node_width);
                    int y_interval = 20;

                    foreach (string mir_id in mirs)
                    {
                        //SizeF sizef = g.MeasureString(mir_id, font);
                        ElementList list = resultsegs[mir_id].getSeg();
                        List<Element> elements = list.getRemappedElements();
                        int i = 0;
                        foreach (Element element in elements)
                        {                            
                            int x1, x2, x3, y1, y2, y3;
                            if (element.getOrientation() == 1)
                            {
                                x1 = (int)(left_x + i * gene_width);
                                y1 = top_y + j * (gene_height + y_interval);
                                x2 = x1;
                                y2 = y1 + gene_height;
                                x3 = (int)(x1 + tri_width);
                                y3 = (int)(y1 + gene_height / 2);
                            }
                            else
                            {
                                x1 = (int)(left_x + i * gene_width + tri_width);
                                y1 = top_y + j * (gene_height + y_interval);
                                x2 = x1;
                                y2 = y1 + gene_height;
                                x3 = (int)(left_x + i * gene_width);
                                y3 = (int)(y1 + gene_height / 2);

                            }
                            int[] x = { x1, x2, x3 };
                            int[] y = { y1, y2, y3 };
                            element.setPolygon(x, y);
                            element.setCircleX(left_x + i * gene_width + tri_width/2);
                            element.setCircleY(y3);

                            if (element.getGene().getID().Equals(mir_id))
                            {
                                int text_x = (int)(left_x + i * gene_width);
                                int text_y = y2;
                                texts.Add(new Text(mir_id, text_x, text_y));
                            }
                            i++;                            
                        }
                        j++;
                    }
                }
                else
                {
                    double degree = 0;
                    double degree_gene = (360 - interval * resultsegs.Count) / gene_num;
                    foreach (string mir_id in mirs)
                    {
                        SizeF sizef = g.MeasureString(mir_id, font);

                        ElementList list = resultsegs[mir_id].getSeg();
                        List<Element> elements = list.getRemappedElements();
                        foreach (Element element in elements)
                        {
                            int x1=0, x2=0, x3=0, y1=0, y2=0, y3=0;                          
                            if (element.getOrientation() == 1)
                            {
                                x1 = (int)(width / 2 + (radius - node_height / 2) * Math.Cos((degree + degree_gene * (0.5 - node_width / 2)) / 180 * Math.PI));
                                y1 = (int)(height / 2 - (radius - node_height / 2) * Math.Sin((degree + degree_gene * (0.5 - node_width / 2)) / 180 * Math.PI));
                                x2 = (int)(width / 2 + (radius + node_height / 2) * Math.Cos((degree + degree_gene * (0.5 - node_width / 2)) / 180 * Math.PI));
                                y2 = (int)(height / 2 - (radius + node_height / 2) * Math.Sin((degree + degree_gene * (0.5 - node_width / 2)) / 180 * Math.PI));
                                x3 = (int)(width / 2 + radius * Math.Cos((degree + degree_gene * (0.5 + node_width / 2)) / 180 * Math.PI));
                                y3 = (int)(height / 2 - radius * Math.Sin((degree + degree_gene * (0.5 + node_width / 2)) / 180 * Math.PI));                         
                            }
                            else
                            {
                                x1 = (int)(width / 2 + (radius - node_height / 2) * Math.Cos((degree + degree_gene * (0.5 + node_width/2)) / 180 * Math.PI));
                                y1 = (int)(height / 2 - (radius - node_height / 2) * Math.Sin((degree + degree_gene * (0.5 + node_width / 2)) / 180 * Math.PI));
                                x2 = (int)(width / 2 + (radius + node_height / 2) * Math.Cos((degree + degree_gene * (0.5 + node_width / 2)) / 180 * Math.PI));
                                y2 = (int)(height / 2 - (radius + node_height / 2) * Math.Sin((degree + degree_gene * (0.5 + node_width / 2)) / 180 * Math.PI));
                                x3 = (int)(width / 2 + radius * Math.Cos((degree + degree_gene * (0.5 - node_width/2)) / 180 * Math.PI));
                                y3 = (int)(height / 2 - radius * Math.Sin((degree + degree_gene * (0.5 - node_width/2)) / 180 * Math.PI));
                            }
                            int[] x = { x1, x2, x3 };
                            int[] y = { y1, y2, y3 };
                            element.setPolygon(x, y);
                            element.setCircleX((double)width / 2 + radius * Math.Cos((degree + degree_gene / 2) / 180 * Math.PI));
                            element.setCircleY((double)height / 2 - radius * Math.Sin((degree + degree_gene / 2) / 180 * Math.PI));

                            if (element.getGene().getID().Equals(mir_id))
                            {

                                if (degree >= 0 && degree < 90)
                                {
                                    //texts.Add(new Text(mir_id, x2 + 5, y2 - 25));
                                    texts.Add(new Text(mir_id, x2, y2));
                                }
                                else if (degree >= 90 && degree < 180)
                                {
                                    //texts.Add(new Text(mir_id, x2 - 120, y2 - 25));

                                    texts.Add(new Text(mir_id, x2 - (int)sizef.Width, y2));
                                }
                                else if (degree >= 180 && degree < 270)
                                {
                                    //texts.Add(new Text(mir_id, x2 - 120, y2 + 10));
                                    texts.Add(new Text(mir_id, x2 - (int)sizef.Width, y2));
                                }
                                else
                                {
                                    //texts.Add(new Text(mir_id, x2 + 5, y2 + 10));
                                    texts.Add(new Text(mir_id, x2, y2));
                                }
                            }
                            degree += degree_gene;

                        }
                        degree += interval;
                    }
                }                
            }
            else
            {
                MessageBox.Show("The list is empty");
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
               
    }

   
}
