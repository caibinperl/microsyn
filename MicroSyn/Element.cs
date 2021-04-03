using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MirSyn
{
    public class Element
    {
        Gene gene;
        int orientation;              
        string mir_id;
        double circleX, circleY; 
        Point[] poly = new Point[3];
        HashSet<Element> homologys = new HashSet<Element>();
        bool isGap = false;

        public Element(Gene gene, int orientation)
        {
            this.gene = gene;
            this.orientation = orientation;           
        }

        public Element()
        {
            this.gene = new Gene("gap", 1);
            this.orientation = 1;     
        
            isGap = true;           
        }

        //Method

        // Add a point to another element into homologys
        public void addHomologyElement(Element element)
        {
            homologys.Add(element);
        }

        // fills in the queue with integers corresponding to pairs with the element
        public void homologyPositions(List<Element> elements, List<int> q)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (gene.isHomologyWith(elements[i].getGene()))
                {
                    q.Add(i);
                }
            }
        }

        public void invertOrientation() { orientation = orientation == 1 ? 0 : 1; }

        //Set        
        public void setPolygon(int[] x, int[] y)
        {
            poly[0] =  new Point(x[0], y[0]);
            poly[1] = new Point(x[1], y[1]);
            poly[2] = new Point(x[2], y[2]);            
        }
      
        public double getCircleX() { return circleX; }
        public double getCircleY() { return circleY; }
        public void setCircleX(double x) { circleX = x; }
        public void setCircleY(double y) { circleY = y; }
        public void setMirID(string mir_id) { this.mir_id = mir_id; }
        
        //Get
        public int getOrientation() {return orientation;}       
        public Point[] getPolygon() { return poly; }
        public String getMirID() { return mir_id; }
        public Gene getGene() { return gene; }
        public HashSet<Element> getHomologyElements() { return homologys; }

        public bool getIsGap() { return isGap; }

    }
}
