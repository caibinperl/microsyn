using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MirSyn
{
    public class HomologyPoint
    {
        Gene gene_x;
        Gene gene_y;
        //belong to which baseclusgter
        BaseCluster basecluster;
        // Index in gene list
        int x, y;
        public HomologyPoint(int x, int y, BaseCluster basecluster)
        {
            this.basecluster = basecluster;
            this.x = x;
            this.y = y;
        }

        //mirrors the y-value around the center
        public void twistY(int max_y, int min_y)
        {
            y = max_y + min_y - y;
        }

        //Set
        public void setGenes(Gene gene_x, Gene gene_y)
        {
            this.gene_x = gene_x;
            this.gene_y = gene_y;
        }

        //Get
        public int getX() { return x; }
        public int getY() { return y; }
        public Gene getGeneX() { return gene_x; }
        public Gene getGeneY() { return gene_y; }
        public BaseCluster getBaseCluster() { return basecluster; } 
    }
}
