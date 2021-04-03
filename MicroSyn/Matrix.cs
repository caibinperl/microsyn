using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace MirSyn
{
    public class Matrix
    {
        Data data;
        // The two lists from which the matrix was constructed
        ElementList x_object;
        ElementList y_object;

        string x_mir;
        string y_mir;
        int count_points = 0;
        int area = 0;
        bool identical = false;

        Cluster cluster; //Store + and - basecluster 
        
        //<x, <y1,y2,y3> >
        Dictionary<int, HashSet<int>>[] origin_matrixs = new Dictionary<int, HashSet<int>>[2];
        Dictionary<int, List<int>>[] matrixs = new Dictionary<int, List<int>>[2]; //
              
        List<BaseCluster>[] baseclusters = new List<BaseCluster>[2];

        //Construct
        public Matrix(Data data, string x_mir, string y_mir, ElementList x_object, ElementList y_object)
        {
            this.data = data;
            this.x_object = x_object;
            this.y_object = y_object;
            this.x_mir = x_mir;
            this.y_mir = y_mir;
            for (int i = 0; i < 2; i++)
            {
                origin_matrixs[i] = new Dictionary<int, HashSet<int>>();
            }

            for (int i = 0; i < 2; i++)
            {
                matrixs[i] = new Dictionary<int, List<int>>();
            }

            for (int i = 0; i < 2; i++)
            {
                baseclusters[i] = new List<BaseCluster>();
            }
        }

        //
        public void buildMatrix()
        {
            List<Element> x_elements = x_object.getRemappedElements();
            List<Element> y_elements = y_object.getRemappedElements();
            for (int x = 0; x < x_elements.Count; x++)
            {
                if (x_elements[x].getGene().hasHomologys())
                {
                    int max_y = y_elements.Count;
                    List<int> queue = new List<int>();
                    x_elements[x].homologyPositions(y_elements, queue);                                      
                    foreach (int y in queue)
                    {                       
                        if (y < max_y)
                        {
                            if (x_elements[x].getOrientation() == y_elements[y].getOrientation())
                            {
                                if (origin_matrixs[1].ContainsKey(x))
                                {
                                    HashSet<int> temp = origin_matrixs[1][x];
                                    temp.Add(y);
                                }
                                else
                                {
                                    HashSet<int> temp = new HashSet<int>();
                                    temp.Add(y);
                                    origin_matrixs[1].Add(x, temp);
                                }
                            }
                            else
                            {
                                if (origin_matrixs[0].ContainsKey(x))
                                {
                                    HashSet<int> temp = origin_matrixs[0][x];
                                    temp.Add(y);
                                }
                                else
                                {
                                    HashSet<int> temp = new HashSet<int>();
                                    temp.Add(y);
                                    origin_matrixs[0].Add(x, temp);                                    
                                }
                            }
                            count_points++;
                        }
                    }
                }
            }           
            copyMatrixs();
        }

        public void run()
        {
            if (count_points >= 3) //count_points is caculated in buildMatrix()
            {
                prepareForStatisticalValidation();
                int[] gapsizes = new int[10];
                data.getGapSizes(gapsizes);
                int i = 0;
                while (i < 10 && gapsizes[i] != -100)
                {
                    for (int j = 1; j >= 0; j--)
                    {
                        detectBaseClusters(gapsizes[i], j, data.getQValue());
                        enrichBaseClusters(gapsizes[i], j, j, data.getQValue());               
                    }
                    i++;
                }
                joinBaseClusters(0);
                joinBaseClusters(1);
                enrichBaseClusters(data.getGapSize(), 0, 0, data.getQValue());
                enrichBaseClusters(data.getGapSize(), 1, 1, data.getQValue());
                probBaseClusters();
                clusterBaseClusters(data.getClusterGap(), data.getQValue(), 3);
                filterClusters();
            }
        }
          
        protected void prepareForStatisticalValidation()
        {
            area = x_object.getSize() * y_object.getSize();
        }

        private void copyMatrixs()
        {            
            for (int i = 0; i < 2; i++)
            {                         
                foreach(KeyValuePair<int, HashSet<int>> pair in origin_matrixs[i])
                {                   
                    HashSet<int> set = pair.Value;
                    List<int> list_temp = new List<int>(set.Count);                    
                    foreach(int el in set)
                    {
                        list_temp.Add(el);
                    }
                    matrixs[i].Add(pair.Key, list_temp);
                }
            }            
        }

        private void detectBaseClusters(int gap, int orientation, double qValue) 
        {
            Dictionary<int,List<int>>.KeyCollection key_c = matrixs[orientation].Keys;         
            List<int> keys = new List<int>(key_c.Count);
            foreach(int key in key_c)
            {
                keys.Add(key);
            }           
            int sign = 0;
            if (orientation == 1){
                sign = 1;
            }
            else if (orientation == 0)
            {
                sign = -1;
            }
            else
            {
                MessageBox.Show("There are wront in Matrix's orientation!");
            }
            

            //get index of remapped_elements by gene name
            int mir_x = x_object.getCoordByGeneName(x_mir);
            int mir_y = y_object.getCoordByGeneName(y_mir);
           
            if( mir_x == -1 || mir_y == -1)
            {
                MessageBox.Show("Failed to find the coord of genes!");               
            }         
            BaseCluster basecluster = new BaseCluster (x_object, y_object, orientation);       
            basecluster.addHomologyPoint(mir_x, mir_y);

            //ref_x, ref_y start point every  search cirle
            int ref_x = mir_x;
            int ref_y = mir_y;
            while (ref_x >= 0) 
            {
                bool found = false;
                int closest_x = 0, closest_y = 0;
                double closest_distance = gap + 1;
                for (int x = ref_x + 1; x <= ref_x + gap; x++)
                {
                    if (matrixs[orientation].ContainsKey(x))
                    {
                        List<int> ys = matrixs[orientation][x];
                        for (int y = ref_y + sign; Math.Abs(ref_y - y) <= gap; y += sign)
                        {                            
                            if (ys.Contains(y))
                            {
                                double distance = basecluster.dpd(ref_x, ref_y, x, y);
                                if (distance < closest_distance) 
                                {
                                    closest_distance = distance;
                                    closest_x = x;
                                    closest_y = y;
                                    found = true;
                                }
                            }
                        }
                    }
                }
                if (found)
                {                    
                    basecluster.addHomologyPoint(closest_x, closest_y);
                    ref_x = closest_x;
                    ref_y = closest_y;
                }
                else 
                {
                    ref_x = -1;
                }
            }
            ref_x = mir_x;
            ref_y = mir_y;
            while (ref_x >= 0) 
            {
                bool found = false;
                int closest_x = 0, closest_y = 0;
                double closest_distance = gap + 1;
                for (int x = ref_x - 1; x >= ref_x - gap; x--)
                {
                    if (matrixs[orientation].ContainsKey(x))
                    {
                        List<int> ys = matrixs[orientation][x];
                        for (int y = ref_y - sign; Math.Abs(ref_y - y) <= gap; y -= sign) 
                        {                          
                            if (ys.Contains(y)) 
                            {
                                double distance = basecluster.dpd(ref_x, ref_y, x, y);
                                if (distance < closest_distance) 
                                {
                                    closest_distance = distance;
                                    closest_x = x;
                                    closest_y = y;
                                    found = true;
                                }
                            }
                        }
                    }
                }
                if (found) 
                {                   
                    basecluster.addReverseHomologyPoint(closest_x, closest_y);
                    ref_x = closest_x;
                    ref_y = closest_y;
                }
                else 
                {
                    ref_x = -1;
                }
            }
            if (basecluster.getCountHomologyPoints() > 2) 
            {                
                basecluster.setMirCoordinate(mir_x, mir_y);                               
                this.addBaseCluster(basecluster);
                basecluster.confidenceInterval();
            } 
            
        }

        /* searches for anchorpoints to be inserted into a basecluster that are
          located insided the gap size
        */
        private void enrichBaseClusters(int gap, int cluster_orientation, int matrix_orientation, double qValue)
        {
            List<BaseCluster> base_clusters = baseclusters[cluster_orientation];
            Dictionary<int,List<int>> matrix = matrixs[matrix_orientation];
            if (base_clusters.Count != 0)
            {
                foreach (KeyValuePair<int, List<int>> pair in matrix)
                {
                    int x = pair.Key;
                    List<int> ys = pair.Value;                    
                    for(int i = ys.Count - 1; i >= 0; i--)
                    {
                        int y = ys[i];
                        BaseCluster closest_cluster = null;
                        double closest_distance = gap + 1;
                        foreach (BaseCluster basecluster in base_clusters)
                        {                               
                            basecluster.confidenceInterval();
                            double distance = basecluster.distanceToPoint(x,y);
                            if (distance < closest_distance && basecluster.r_squared(x,y) >= qValue
                                        && basecluster.inInterval(x,y) && !basecluster.isExistPoint(x,y) )
                            {
                                closest_distance = distance;
                                closest_cluster = basecluster;
                            }
                        }
                        if (closest_cluster != null)
                        {                            
                            closest_cluster.addHomologyPoint(x, y);
                            //ys.Remove(y);
                            closest_cluster.confidenceInterval();
                        }                       
                    }
                }
            }
        }

        private void joinBaseClusters(int orientation)
        {
            List<BaseCluster> base_clusters = baseclusters[orientation];
            if(base_clusters.Count != 0)
            {
                BaseCluster base_cluster = base_clusters[0];
                for(int i=base_clusters.Count-1; i>0; i--)
                {
                    base_cluster.mergeWith(base_clusters[i]);
                    base_clusters.RemoveAt(i);
                }
            }
        }
       

        private void addBaseCluster(BaseCluster basecluster)
        {
            baseclusters[basecluster.getOrientation()].Add(basecluster);            
        }

        private void addCluster(Cluster cluster)
        {
            this.cluster = cluster;
        }

      
        /* performs statistical filtering on all baseclusters.
           clusters with a probability higher than the specified cutoff are removed.
           the probability is stored in the remaining clusters 
         */
        void probBaseClusters()
        {
            for (int i = 0; i < 2; i++)
            {               
                foreach(BaseCluster basecluster in baseclusters[i])
                {                   
                    basecluster.calculateProbability(area, count_points);
                }
            }
        }

        void filterClusters()
        {
           
            if(cluster != null){

                double probe = cluster.getProbability();
                if (probe > data.getProbCutoff() || cluster.getCountHomologyPoints() < data.getHomologyPoints())
                {
                    cluster = null;
                }          
            }
            
        }
        

        protected void clusterBaseClusters(int clusterGap, double qValue, int cntHomologypoints)
        {
            if (baseclusters[0].Count != 0 && baseclusters[1].Count != 0)
            {
                double prob0, prob1;
                Cluster cluster = new Cluster(x_object, y_object);               
                prob0 = baseclusters[0][0].getProbability();
                         
                prob1 = baseclusters[1][0].getProbability();
               
                if (prob1 >= prob0)
                {
                     cluster.addBaseCluster(baseclusters[1][0]);
                }
                else
                {
                     cluster.addBaseCluster(baseclusters[1][0]);    
                }
                cluster.setMirX(x_mir);
                cluster.setMirY(y_mir);
                addCluster(cluster);
            } else if (baseclusters[0].Count != 0 && baseclusters[1].Count == 0) 
            {
                Cluster cluster = new Cluster(x_object, y_object);
                cluster.addBaseCluster(baseclusters[0][0]);
                cluster.setMirX(x_mir);
                cluster.setMirY(y_mir);
                addCluster(cluster);
            }
            else if (baseclusters[0].Count == 0 && baseclusters[1].Count != 0)
            {
                Cluster cluster = new Cluster(x_object, y_object);
                cluster.addBaseCluster(baseclusters[1][0]);
                cluster.setMirX(x_mir);
                cluster.setMirY(y_mir);
                addCluster(cluster);
            }
        }

        //Set
        public void setIdentical(bool identical) { this.identical = identical;}


        //Get
        public ElementList getXObject() { return x_object; }
        public ElementList getYObject() { return y_object; }
        public int getXObjectSize() { return x_object.getRemappedElementsLength(); }
        public int getYObjectSize() { return y_object.getRemappedElementsLength(); }
        public int getNumberOfPoints() { return count_points; }
        public Dictionary<int, List<int>>[] getMatrixs() { return matrixs; }
               
        public Cluster getCluster()
        {
            if (cluster != null)
            {
                List<Element> x_elements = x_object.getRemappedElements();
                List<Element> y_elements = y_object.getRemappedElements();
                cluster.setBounds();
                List<BaseCluster> baseclusters = cluster.getBaseClusters();
                for (int j = 0; j < baseclusters.Count; j++)
                {
                    baseclusters[j].setBounds();
                    List<HomologyPoint> homologypoints = baseclusters[j].getHomologyPoints();
                    for (int k = 0; k < homologypoints.Count; k++)
                    {
                        int x = homologypoints[k].getX();
                        int y = homologypoints[k].getY();
                        homologypoints[k].setGenes(x_elements[x].getGene(), y_elements[y].getGene());
                    }
                }
                return cluster;
            }
            return null;                  
        }
    }
}
