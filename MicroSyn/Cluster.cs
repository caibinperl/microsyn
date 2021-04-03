using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MirSyn
{
    public class Cluster
    {
        ElementList x_object;
        ElementList y_object;
        List<BaseCluster> baseclusters = new List<BaseCluster>();
        int id = 0;
        //The lowest and highest X, Y coordinates
        int begin_x, end_x, begin_y, end_y;
        string x_mir, y_mir;

        public Cluster(ElementList x_object, ElementList y_object)
        {
            this.x_object = x_object;
            this.y_object = y_object;
        }
               

        //adds one single basecluster to this multiplicon
        public void addBaseCluster(BaseCluster basecluster)
        {
            basecluster.setCluster(this);
            baseclusters.Add(basecluster);
        }

        //clears the multiplicon from all baseclusters but does not free the memory!!
        public void clear() { baseclusters.Clear(); }

        //Set
        public void setId(int id) {this.id = id;}
        public void setMirX(String mir_x) { this.x_mir = mir_x; }
        public void setMirY(String mir_y) { this.y_mir = mir_y; }

        public void setBounds()
        {
            begin_x = getLowestX();
            begin_y = getLowestY();
            end_x = getHighestX();
            end_y = getHighestY();
        }

        //Get
        public ElementList getXObject() { return x_object; }
        public ElementList getYObject() { return y_object; }
        public string getMirX() { return x_mir; }
        public string getMirY() { return y_mir; }
        public int getBeginX() { return begin_x; }
        public int getEndX() { return end_x; }
        public int getBeginY() { return begin_y; }
        public int getEndY() { return end_y; }
        public List<BaseCluster> getBaseClusters() { return baseclusters; }
        //returns the id of the multiplicon
        public int getId() { return id; }       

        //calculates and returns the total number of anchorpoints from all baseclusters in this multiplicon
        public int getCountHomologyPoints()
        {
            int size = 0;
            for (int i = 0; i < baseclusters.Count; i++)
            {
                size += baseclusters[i].getCountHomologyPoints();
            }
            return size;
        }

        //returns the lowest x-value from all anchorpoints
        public int getLowestX()
        {
            int lowest_x = baseclusters[0].getLowestX();
            for (int j = 1; j < baseclusters.Count; j++)
            {
                if (baseclusters[j].getCountHomologyPoints() > 0)
                {
                    int x = baseclusters[j].getLowestX();
                    if (x < lowest_x)
                        lowest_x = x;
                }
            }
            return lowest_x;
        }

        //returns the highest x-value from all anchorpoints
        public int getLowestY()
        {
            int lowest_y = baseclusters[0].getLowestY();
            for (int j = 1; j < baseclusters.Count; j++)
            {
                if (baseclusters[j].getCountHomologyPoints() > 0)
                {
                    int y = baseclusters[j].getLowestY();
                    if (y < lowest_y)
                        lowest_y = y;
                }
            }
            return lowest_y;
        }

        //returns the lowest y-value from all anchorpoints
        public int getHighestX()
        {
            int highest_x = baseclusters[0].getHighestX();
            for (int j = 1; j < baseclusters.Count; j++)
            {
                if (baseclusters[j].getCountHomologyPoints() > 0)
                {
                    int x = baseclusters[j].getHighestX();
                    if (x > highest_x) highest_x = x;
                }
            }
            return highest_x;
        }

        //returns the highest y-value from all anchorpoints
        public int getHighestY()
        {
            int highest_y = baseclusters[0].getHighestY();
            for (int j = 1; j < baseclusters.Count; j++)
            {
                if (baseclusters[j].getCountHomologyPoints() > 0)
                {
                    int y = baseclusters[j].getHighestY();
                    if (y > highest_y) highest_y = y;
                }
            }
            return highest_y;
        }      

        public double getProbability()
        {
            double probability = baseclusters[0].getProbability();
            for (int i = 1; i < baseclusters.Count; i++)
            {
                probability = probability * baseclusters[i].getProbability();
            }
            return probability;
        }
    }
}
