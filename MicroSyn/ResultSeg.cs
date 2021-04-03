using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MirSyn
{
    public class ResultSeg
    {
        string mir_id;
        int maxCoord, minCoord;
        ElementList list;
        ElementList seg;

        public ResultSeg(string mir_id, ElementList list, int min_x, int max_x)
        {
            this.list = list;
            this.mir_id = mir_id;
            this.minCoord = min_x;
            this.maxCoord = max_x;
        }

        public void createSeq()
        {
            seg = new ElementList(list, minCoord, maxCoord);
            List<Element> es = seg.getRemappedElements();
            for (int i = 0; i < es.Count; i++)
            {
                es[i].setMirID(mir_id);
            }
        }

        //Set
        public void setMaxCoord(int max_x){ this.maxCoord = max_x;}
        public void setMinCoord(int min_x){ this.minCoord = min_x; }

        //Get
        public ElementList getSeg() { return seg; }
        public int getMaxCoord() { return maxCoord; }
        public int getMinCoord() { return minCoord; }

        public int getCountHomologys()
        {
            int count = 0;
            List<Element> elements = seg.getRemappedElements();
            for (int i = 0; i < elements.Count; i++)
            {
                count += elements[i].getHomologyElements().Count;
            }
            return count;
        }
    }
}
