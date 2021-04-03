using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MirSyn
{
    public class BaseCluster
    {        
        // The two lists from which the basecluster was constructed
        ElementList x_object;
        ElementList y_object;
        
        /* X -----begin_x-------end_x-----
         * Y -----begin_y-------end_y-----
        */
        int begin_x, begin_y, end_x, end_y;
        int orientation;
        bool was_twisted = false;
        int id = 0;
        
        //mir coordinates
        int mirx_i, miry_i;
        
        //
        List<HomologyPoint> homologypoints = new List<HomologyPoint>();
        
        //belong to which cluster
        Cluster cluster = null;
        
        bool isCore = false;
        double prob = 0;   
        
        //coefficients of the best fit line
        double a, b = 0.0;
        double avg_x, var_x, variance;
        
        /*
        * X ---x_end1------x_end2-------
        * Y ---y_end1------y_end2-------
        */
        double x_end1, x_end2, y_end1, y_end2;    
    
        //Constructor
        public BaseCluster(ElementList x_object, ElementList y_object, int orientation) {
            this.x_object = x_object;
            this.y_object = y_object;
            this.orientation = orientation;
        }
    
        /* Creates an homologypoint from the specified x and y values
        * and is placed into the ArrayList of homologypoints
        */
        public void addHomologyPoint(int X, int Y) {
            homologypoints.Add(new HomologyPoint(X, Y, this));
            b = 0.0; //indicates the statistics need to be updated
        }   

        // add into the front position in list
        public void addReverseHomologyPoint(int X, int Y) {
            homologypoints.Insert(0, new HomologyPoint(X, Y, this));
            b = 0.0; //indicates the statistics need to be updated
        }   

        // Sorts the ArrayList of homologypoints by x coordinate (go up)
        public void sortHomologyPoints() 
        {
            for (int i = 1; i < homologypoints.Count; i++)
            {
                HomologyPoint tmp = homologypoints[i];
                int j = i;
                while (j > 0 && tmp.getX() < homologypoints[j-1].getX())
                {
                    homologypoints[j] = homologypoints[j-1];
                    j--;
                }
                homologypoints[j] = tmp;
            }
        }
        

        public double dpd(double x1, double y1, double x2, double y2) 
        {
            if (Math.Abs(x1-x2) < Math.Abs(y1-y2)) 
            {
                double t;
                t = x1; x1 = y1; y1 = t;
                t = x2; x2 = y2; y2 = t;
            }
            return (2 * Math.Abs(x1 - x2) - Math.Abs(y1 - y2));
        }   
    
       
        /*
        *Merges the basecluster with the specified basecluster which anchorpoints
        *are inserted into the basecluster.
        *the basecluster is deleted afterwards.
        */
        public void mergeWith(BaseCluster basecluster)
        {
            for (int i = 0; i < basecluster.getHomologyPoints().Count; i++) 
            {
                int x = basecluster.getHomologyPoints()[i].getX();
                int y = basecluster.getHomologyPoints()[i].getY();
                if (!this.isExistPoint(x,y))
                {
                    addHomologyPoint(x, y);
                }
            }
            
        }
    
        /*
        *Returns a boolean saying the specified x and y coordinate is
        *located inside the confidence interval of the basecluster
        */
        public bool inInterval(int x, int y)
        {
            double[] ys = new double[2];
            intervalBounds(x, ys);
            if (y >= ys[0] && y <= ys[1]) 
            {
                return true;
            } 
            else 
            {
                return false;
            }
        }   

        //Returns for a given x coordinate the upper and lower y coordinates of the confidence interval
        public void intervalBounds(int x, double[] ys) 
        {
            confidenceInterval();
            // t(n-2)
            double lambda = alglib.studenttdistr.invstudenttdistribution(homologypoints.Count - 2, (0.01) / 2);
            lambda = Math.Abs(lambda);
            double sdev_y_est = Math.Sqrt(variance * (1 + 1 / (double)homologypoints.Count +  (x - avg_x) * (x - avg_x) / var_x) );
            ys[0] = (double) ((a + b * x) - (lambda * sdev_y_est));
            ys[1] = (double)((a + b * x) + (lambda * sdev_y_est));
        }

        //Calculates the distance from the basecluster to the specified x and y coordinate
        public double distanceToPoint(int x, int y)
        {
            if (x >= x_end1 && x <= x_end2) 
            {
                return 0;
            } 
            else if ((double)x < x_end1) 
            {
                return dpd((double)x, (double)y, x_end1, y_end1);
            }
            else
            {
                return dpd((double)x, (double)y, x_end2, y_end2);
            }
        }

        // Calculates the distance from this basecluster to the specified basecluster
        public double distanceToCluster(BaseCluster basecluster) 
        {
		    confidenceInterval();
		    basecluster.confidenceInterval();
		    double lowest_distance = 0;
		    if (overlappingCoordinates(basecluster) != 2) 
            {
                lowest_distance = dpd(x_end1, y_end1, basecluster.getXEnd1(), basecluster.getYEnd1());
                double d = dpd(x_end1, y_end1, basecluster.getXEnd2(), basecluster.getYEnd2());
                if (d < lowest_distance) 
                {
                    lowest_distance = d;
                }
                d = dpd(x_end2, y_end2, basecluster.getXEnd2(), basecluster.getYEnd2());
                if (d < lowest_distance) 
                {
                    lowest_distance = d;
                }
                d = dpd(x_end2, y_end2, basecluster.getXEnd1(), basecluster.getYEnd1());
                if (d < lowest_distance)
                {
                    lowest_distance = d;
                }
            }
            return lowest_distance;
        }


        /*
        Returns 1 if either the x or y coordinates overlap the specified cluster
        Returns 2 if both the x and y coordinates overlap the specified cluster
        Returns 0 if nothing overlaps
        */
        public int overlappingCoordinates(BaseCluster basecluster)
        {
		    int overlap_status = 0;
		    double ax1 = x_end1, ay1 = y_end1, ax2 = x_end2, ay2 = y_end2;
		    double bx1 = basecluster.getXEnd1(), by1 = basecluster.getYEnd1(), bx2 = basecluster.getXEnd2(), by2 = basecluster.getYEnd2();
		    if (orientation == 0) 
            { 
                ay1 = y_end2; 
                ay2 = y_end1;
            }
		    if (getOrientation() == 0) 
            {
                by1 = basecluster.getYEnd2(); 
                by2 = basecluster.getYEnd1();
            }
		    if (ax1 < bx2 && ax2 > bx1)
            {
                overlap_status++;
            }
		    if (ay1 < by2 && ay2 > by1) 
            {
                overlap_status++;
            }
		    return overlap_status;
        }

        //Returns a boolean saying if the specified basecluster lies within the confidence interval of this basecluster
        public bool overlappingInterval(BaseCluster basecluster)
        {
            bool overlapping = true;
            confidenceInterval();
            basecluster.confidenceInterval();
            //first, check if all anchorpoints of cluster a lie within the confidence interval of cluster b
            for(int i = 0; i < homologypoints.Count && overlapping; i++)
            {
                if (!(basecluster.inInterval( homologypoints[i].getX(), homologypoints[i].getY()))) 
                {
                    overlapping = false;
                }
            }
            //if not overlapped yet, check again but the other way
            if (!overlapping) 
            {
                overlapping = true;
                for(int j = 0; j < basecluster.homologypoints.Count; j++)
                {
                    if (!inInterval(basecluster.homologypoints[j].getX(), basecluster.homologypoints[j].getY()))
                    {
                        overlapping = false;
                    }
                }
            }
            return overlapping;
        }

        /*Calculates the squared Pearson value for all x and y coordinates of all homologypoints,
        *including the specified values.
        */
        public double r_squared(int X, int Y) 
        {
            double sum_x = 0, sum_y = 0, sum_xy = 0, sum_x2 = 0, sum_y2 = 0;
            int n = 0;
            for (int i = 0; i < homologypoints.Count; i++) 
            {
                int x = homologypoints[i].getX();
                int y = homologypoints[i].getY();
                sum_x += x;
                sum_y += y;
                sum_xy += x*y;
                sum_x2 += x*x;
                sum_y2 += y*y;
                n++;
            }
            //if X and Y are given as arguments, count them too
            if (X != 0 || Y != 0) 
            {
                sum_x += X;
                sum_y += Y;
                sum_xy += X*Y;
                sum_x2 += X*X;
                sum_y2 += Y*Y;
                n++;
		    }
		    double value = ((sum_x2 - sum_x * sum_x / n) * (sum_y2 - sum_y * sum_y / n));
		    if (value == 0) 
            {
                return -1;
		    } else 
            {
                double r = (sum_xy - (sum_x * sum_y / n)) / Math.Sqrt(value);
                return (r * r);
		    }
        }

        /*Calculates the squared Pearson value for all x and y coordinates of all
        *anchorpoints, including all anchorpoints from the specified cluster
        */
        public double r_squared(BaseCluster basecluster) 
        {
		    double sum_x = 0, sum_y = 0, sum_xy = 0, sum_x2 = 0, sum_y2 = 0;
		    int n = 0;
		    for (int i = 0; i < homologypoints.Count; i++)
            {
                int x = homologypoints[i].getX();
                int y = homologypoints[i].getY();
                sum_x += x; sum_y += y;
                sum_xy += x*y;
                sum_x2 += x*x;
                sum_y2 += y*y;
                n++;
		    }
		    //count the cluster too
		    for (int i = 0; i < basecluster.homologypoints.Count; i++) 
            {
                int x = basecluster.homologypoints[i].getX();
                int y = basecluster.homologypoints[i].getY();
                sum_x += x; sum_y += y;
                sum_xy += x*y;
                sum_x2 += x*x;
                sum_y2 += y*y;
                n++;
		    }
            double value = ((sum_x2 - sum_x * sum_x / n) * (sum_y2 - sum_y * sum_y / n));
		    if (value == 0) 
            {
                return -1;
		    } 
            else 
            {
                double r = (sum_xy - (sum_x * sum_y / n)) / Math.Sqrt(value);
                return (r * r);
		    }
        }


        /*
        * Calculates the probability to be generated by chance.
        * the area and number of points in the GHM needs to be passed as arguments
        */

        public void calculateProbability(int area, int points)
        {       
            double c;
            double density = (double)points / area;
            double p_i;
            double probability = 1.0;
            double distance;
            sortHomologyPoints();
            int mir_index = 0;
            for(int i = 0; i < homologypoints.Count; i++)
            {
                if (homologypoints[i].getX() == mirx_i)
                {
                    mir_index = i;
                }
            }

            for (int i = mir_index; i > 0; i--) 
            {
                if(homologypoints[i-1] != null && homologypoints[i] != null)
                {
                    distance = dpd(homologypoints[i-1].getX(), homologypoints[i-1].getY(),
                                                homologypoints[i].getX(), homologypoints[i].getY());
                    if (distance == 0) 
                    {
                        homologypoints[i-1] = null;
                    } 
                    else
                    {
                        c = Math.Ceiling((double)distance * distance / 2);
                        p_i = (double)(c * density * Math.Pow(1 - density, c - 1));                   
                        probability *= p_i;
                    }
                }
            } 
            for (int i = mir_index; i < homologypoints.Count - 1; i++)
            {
                if(homologypoints[i+1] != null && homologypoints[i] != null)
                {
                    distance = dpd(homologypoints[i+1].getX(), homologypoints[i+1].getY(),
                                   homologypoints[i].getX(), homologypoints[i].getY());
                    if (distance == 0) 
                    {
                        homologypoints[i+1] = null;
                    } 
                    else
                    {
                        c = Math.Ceiling((double)distance * distance / 2);
                        p_i = (double)(c * density * Math.Pow(1 - density, c - 1));                 
                        probability *= p_i;
                    }
                }
            }
            for (int i = homologypoints.Count - 1; i >= 0; i-- )
            {                
                if (homologypoints[i] == null) 
                {
                    homologypoints.RemoveAt(i);
                }
            }            
            prob = probability;
        }
    

        // Updates all parameters describing the confidence interval of the basecluster
        // n >= 3
        public void confidenceInterval()
        {
            if (b == 0.0) 
            {
                regression();
                var_x = 0;
                variance = 0;
                for (int i = 0; i < homologypoints.Count; i++)
                {
                    int x = homologypoints[i].getX();
                    int y = homologypoints[i].getY();
                    double y_est = b * x + a;
                    variance += (y - y_est) * (y - y_est);
                    var_x += (x - avg_x) * (x - avg_x);
                }
                variance = variance / (homologypoints.Count - 2); //MS_E            
                x_end1 = getLowestX();
                x_end2 = getHighestX();
                y_end1 = x_end1 * b + a;
                y_end2 = x_end2 * b + a;
            }
        }

        // regression coefficient 
        void regression()
        {
            if (homologypoints.Count >= 2) 
            {
                int sum_x = 0, sum_y = 0, sum_xy = 0, sum_x2 = 0;
                for (int i = 0; i < homologypoints.Count; i++)
                {
                    int x = homologypoints[i].getX();
                    int y = homologypoints[i].getY();
                    sum_x += x;
                    sum_y += y;
                    sum_xy += x*y;
                    sum_x2 += x*x;
                }
                avg_x = sum_x / (double)homologypoints.Count;
                double sxx = sum_x2 - sum_x * sum_x / (double)homologypoints.Count;
                double sxy = sum_xy - sum_x * sum_y / (double)homologypoints.Count;
                b = sxy / sxx;
                a = (sum_y - b * sum_x) / homologypoints.Count;
            }
        }       

        public bool isExistPoint(int x, int y)
        {
            foreach (HomologyPoint hp in homologypoints)
            {
               if (hp.getX() == x || hp.getY() == y)
                   return true;
            }
            return false;
        }

        //Set
        public void setCluster(Cluster cluster) { this.cluster = cluster; }
        public void resetCluster() { cluster = null; }
        public void setId(int id) { this.id = id; }
        public void setIsCore(bool isCore) { this.isCore = isCore; }

        public void setMirCoordinate(int mir_x, int mir_y)
        {
            this.mirx_i = mir_x;
            this.miry_i = mir_y;
        }

        public void setBounds()
        {
            begin_x = getLowestX();
            begin_y = getLowestY();
            end_x = getHighestX();
            end_y = getHighestY();
        }

        //Get

        public ElementList getXObject(){ return x_object; }
        public ElementList getYObject(){ return y_object;}
        public int getOrientation(){ return orientation;}
        public List<HomologyPoint>  getHomologyPoints(){ return homologypoints; }   
        public int getCountHomologyPoints() { return homologypoints.Count;}
        public int getBeginX() { return begin_x; }
        public int getEndX() { return end_x; }
        public int getBeginY() { return begin_y; }
        public int getEndY() { return end_y; }
        public double getXEnd1() { return x_end1; }
        public double getXEnd2() { return x_end2; }
        public double getYEnd1() { return y_end1; }
        public double getYEnd2() { return y_end2; }
        public bool getIsCore() { return isCore; }
        public Cluster getCluster() { return cluster; }
        public double getProbability() { return prob; }
        public bool wasTwisted() { return was_twisted; }
        public int getId() { return id; }

        public int getMirx() { return mirx_i; }
        public int getMiry() { return miry_i; }
       
        public int getLowestX() {
            if (homologypoints.Count > 0)
            {
                int lowest_x = homologypoints[0].getX();
                for (int i = 1; i < homologypoints.Count; i++)
                {
                    int x = homologypoints[i].getX();
                    if (x < lowest_x)
                        lowest_x = x;
                }
                return lowest_x;
            } 
            else 
            {
                return 0;
            }
        }

        public int getLowestY()
        {
            if (homologypoints.Count > 0)
            {
                int lowest_y = homologypoints[0].getY();
                for (int i = 1; i < homologypoints.Count; i++) 
                {
                    int y = homologypoints[i].getY();
                    if (y < lowest_y) 
                    {
                        lowest_y = y;
                    }
                }
                return lowest_y;
            }
            else
            {
                return 0;
            }
        }


        public int getHighestX() 
        {
            if (homologypoints.Count > 0)
            {
                int highest_x = homologypoints[0].getX();
                for (int i = 1; i < homologypoints.Count; i++) 
                {
                    int x = homologypoints[i].getX();
                    if (x > highest_x)
                    {
                        highest_x = x;
                    }
                }
                return highest_x;
            } 
            else
            {
                return 0;
            }
        }

        public int getHighestY() 
        {
            if (homologypoints.Count > 0) 
            {
                int highest_y = homologypoints[0].getY();
                for (int i = 1; i < homologypoints.Count; i++) 
                {
                    int y = homologypoints[i].getY();
                    if (y > highest_y) 
                    {
                        highest_y = y;
                    }
                }
                return highest_y;
            }
            else
            {
                return 0;
            }
        }
    }
}
