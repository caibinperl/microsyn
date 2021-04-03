using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MirSyn
{
    class MultiAligner
    {
        Dictionary<string, ResultSeg> resultsegs;
        List<Cluster> clusters;
        Data data;
        List<string> mirs;
        List<List<Element>> leftSegs;
        List<List<Element>> rightSegs;
        List<Element> family;
        List<List<Element>> alignedSegs;
        
        public MultiAligner(Data data)
        {
            this.data = data;
            this.clusters = this.data.getClusters();
            resultsegs = this.data.getResultSegs();
            mirs = new List<string>();
            leftSegs = new List<List<Element>>();
            rightSegs = new List<List<Element>>();
            alignedSegs = new List<List<Element>>();
            family = new List<Element>();
        }

        public void align() 
        {           
            sortMirRegions();
            findSegs();
            alignSegs(rightSegs);
            alignSegs(leftSegs);
            foreach (List<Element> list in leftSegs)
            {
                list.Reverse();
            }
            for (int i = 0; i < mirs.Count; i++ )
            {
                List<Element> alignedSeg = new List<Element>();
                alignedSeg.AddRange(leftSegs[i]);
                alignedSeg.Add(family[i]);
                alignedSeg.AddRange(rightSegs[i]);
                alignedSegs.Add(alignedSeg);
            }            
        }

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

        private void findSegs()
        {
           
            foreach (string mir in mirs) 
            {
                ElementList seg = resultsegs[mir].getSeg();
                List<Element> elements = seg.getRemappedElements();
                     //Search in elements

                Gene mirGene = null;
                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i].getGene().getID().Equals(mir))
                    {
                        mirGene = elements[i].getGene();
                        break;
                    }
                }


                if (mirGene == null)
                {
                    MessageBox.Show("Not found " + mir + " in fragment");
                    
                }
                     
                int strand = mirGene.getOrientation();
               
                if (strand == 0) 
                {                    
                    elements.Reverse();
                }
             
                List<Element> leftSeg = new List<Element>();
                List<Element> rightSeg = new List<Element>();
                bool isLeft = true;
               
                foreach (Element element in elements)
                {
                    if (isLeft == true && element.getGene().getID().Equals(mir))
                    {
                        isLeft = false;
                        family.Add(element);
                        continue;
                    }

                    if (isLeft == true)
                    {
                        leftSeg.Add(element);
                    }
                    else 
                    {
                        rightSeg.Add(element);
                    }
                }
                leftSeg.Reverse();
                leftSegs.Add(leftSeg);
                rightSegs.Add(rightSeg);            
            }
        }

        private int[] findElemet(Element element, List<List<Element>> Segs)
        { 
            int[] pos = {-1, -1};
            for (int i = 0; i < Segs.Count; i++)
            {   
                List<Element> eles = Segs[i];
                for (int j = 0; j < eles.Count; j++ )
                {
                    if (eles[j].getGene().getID().Equals(element.getGene().getID()) && eles[j].getMirID().Equals(element.getMirID()))
                    {
                        pos[0] = i;
                        pos[1] = j;
                    }
                }
            }
            return pos;
        }

        private int findMaxHomo(HashSet<Element> homologyElements, List<List<Element>> Segs)
        {
            int max = 0;
            foreach (Element homo_el in homologyElements)
            {
                int[] pos = findElemet(homo_el, Segs);
                if (pos[1] > max)
                    max = pos[1];
            }
            return max;
        }

        private void alignSegs( List<List<Element>> Segs)
        { 
            int j = 0;
            int end = 0;
            while (end < Segs.Count) 
            {
                for (int i = 0; i  < Segs.Count; i++)
                {
                    if (j > Segs[i].Count - 1) {
                        end++;
                        continue;
                    }                        
                    Element el = Segs[i][j];
                    HashSet<Element> homologyElements = el.getHomologyElements();
                    int max = findMaxHomo(homologyElements, Segs);
                      
                    for (int m = 0; m < max - j; m++)
                    {
                         Segs[i].Insert(j, new Element());
                    }
                    foreach (Element homo in homologyElements)
                    {
                        int[] pos = findElemet(homo, Segs);
                        int x = pos[0];
                        int y = pos[1];
                        if (y == -1)
                        {
                            //MessageBox.Show(homo.getGene().getID() + "not found in segs");
                        }
                        else
                        {

                            for (int m = 0; m < max - y; m++)
                            {
                                Segs[x].Insert(y, new Element());
                            }
                        }
                    }
                }
                j++;            
            }
            int minSegLen = Segs[0].Count;
            foreach (List<Element> list in Segs)
            {
                if (minSegLen > list.Count) 
                    minSegLen = list.Count;
            }
            foreach (List<Element> list in Segs)
            {
                if(list.Count > minSegLen)
                    list.RemoveRange(minSegLen, list.Count - minSegLen);
            }

        }

    
        public void outputAlign(string file)
        {
            try
            {
                StreamWriter sw = new StreamWriter(new FileStream(file, FileMode.Create));
                sw.Write("     " + alignedSegs.Count + "    " + alignedSegs[0].Count + "\n");
                for (int i = 0; i < alignedSegs.Count; i++)
                {
                    sw.Write(mirs[i] + "     ");
                    foreach (Element ele in alignedSegs[i])
                    {
                        if (ele.getIsGap())
                        {
                            sw.Write("0");
                        }
                        else 
                        {
                            sw.Write("1");
                        }
                       
                    }
                    sw.Write("\n");
                }
               
                sw.Flush();
                sw.Close();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }  
        
        }
        
    
    }
}


