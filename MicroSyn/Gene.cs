using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MirSyn
{
    public class Gene
    {
        string ID;
        string chr;
        int position; //start position in GFF        
        int orientation; // 1,0
        int remapped_coordinate;
        bool is_tandem = false;
        bool is_tandem_representative = false;
        string tandem_representative;
        bool remapped = false;
        bool isMirRna = false;
        HashSet<string> homologys = new HashSet<string>();        
        HashSet<string> tandems = new HashSet<string>();

        //Constructor
        public Gene(string ID, string chr, int position, int orientation)
        {
            this.ID = ID;
            this.chr = chr;
            this.position = position;
            this.orientation = orientation;
            tandems.Add(this.ID);
        }

        public Gene(string ID, int orientation)
        {
            this.ID = ID;
            this.orientation = orientation;
            tandems.Add(this.ID);
        }

        
        //Method
      
        //Add a new id to homologys
        public void addHomologys(string id)
        {
            homologys.Add(id);
        }
     

        // remaps the gene onto other gene
        public void remapTo(Gene gene)
        {
            is_tandem = true;
            is_tandem_representative = false;
            tandem_representative = gene.ID;
            remapped = true;
            gene.is_tandem = true;
            gene.is_tandem_representative = true;
            gene.tandem_representative = gene.ID;
            gene.remapped = false;           
            foreach (string id in tandems)
            {
                gene.tandems.Add(id);            
            }           
        }

        //Set
        public void setHomologys(HashSet<string> set)
        {            
            foreach(string id in set)
            {
                homologys.Add(id);            
            }         
        }

        public void setRemappedCoordinate(int remapped_coordinate) {this.remapped_coordinate = remapped_coordinate;}
        public void setIsMirRna(bool isMirRna){this.isMirRna = isMirRna;}

        //Get
        public int getRemappedCoordinate() { return remapped_coordinate; }
        public bool isTandemRepresentative() { return is_tandem_representative; }
        public string getTandemRepresentative() { return tandem_representative; }
        public bool isTandem() { return is_tandem; }
        public string getID() { return ID; }
        public string getChr() { return chr; }
        public int getOrientation() { return orientation; }
        public int getPosition() { return position; }
        public HashSet<string> getHomologys() { return homologys; }
        public HashSet<string> getTandems() { return tandems; }
        public bool IsMirRna() { return isMirRna; }
        // returns true if this gene was remapped
        public bool isRemapped() { return remapped; }
        public bool hasHomologys() { return homologys.Count != 0; }

        public bool isHomologyWith(Gene gene)
        {
            if (hasHomologys() && homologys.Contains(gene.getID()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isIndirectHomologyWith(Gene gene)
        {
            if (hasHomologys() && gene.hasHomologys())
            {
                if (!isHomologyWith(gene))
                {                  
                    foreach (string id in homologys)
                    {
                        if (gene.getHomologys().Contains(id))
                        {
                            return true;
                        }
                    }                    
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
