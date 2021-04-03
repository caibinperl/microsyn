using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MirSyn
{
    public class ElementList
    {
        string list_name;
        List<Element> elements = new List<Element>(); 
        List<Element> remapped_elements = new List<Element>();
        Color color = Color.FromArgb(255, 255, 255);
        
        //Constuct
        public ElementList(string list_name, List<Element> elements)
        {
            this.list_name = list_name;
            this.elements = elements;
        }

        // copy segment list from another list, just including remapped_elements
        public ElementList(ElementList elementlist, int begin, int end)
        {
            this.list_name = elementlist.list_name;
            List<Element> lt = elementlist.getRemappedElements();
            for (int i = begin; i <= end; i++)
            {
                if (i >= 0 && i < elementlist.getRemappedElementsLength())
                {                                        
                    remapped_elements.Add(lt[i]);
                }
            }
        }

        /* performs remapping of tandem repeated genes in a gene list object.
           Genes in a tandem are remapped onto the first gene which is marked as tandem_representative
         */
        public void remapTandemsHomologys(int gap)
        {
            //detect tandem duplicated genes and remap them onto their representatives
            for (int i = elements.Count - 1; i > 0; i--)
            {
                if ( elements[i].getGene().hasHomologys())
                {
                    int j = i - 1;
                    bool found = false;
                    Gene gene_i = elements[i].getGene();
                    while (!found && j >= 0 && j >= i - (gap + 1))
                    {
                        Gene gene_j = elements[j].getGene();
                        if (!gene_i.IsMirRna() && gene_j.hasHomologys() && gene_i.isIndirectHomologyWith(gene_j))
                        {
                            gene_i.remapTo(gene_j);
                            found = true;
                        }
                        j--;
                    }
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (!elements[i].getGene().isRemapped())
                {
                    remapped_elements.Add(elements[i]);
                }
            }
            //calculate remapped coordinate for each gene    
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                remapped_elements[i].getGene().setRemappedCoordinate(i);
            }
        }

        public void remapTandemsMirs(int gap)
        {
            //detect tandem duplicated genes and remap them onto their representatives
            for (int i = elements.Count - 1; i > 0; i--)
            {
                if (elements[i].getGene().hasHomologys())
                {
                    int j = i - 1;
                    bool found = false;
                    Gene gene_i = elements[i].getGene();
                    while (!found && j >= 0 && j >= i - (gap + 1))
                    {
                        Gene gene_j = elements[j].getGene();
                        if (!gene_i.IsMirRna() && gene_j.hasHomologys() && gene_i.isIndirectHomologyWith(gene_j))
                        {

                            gene_i.remapTo(gene_j);
                            found = true;
                        }
                        j--;
                    }
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (!elements[i].getGene().isRemapped())
                {
                    remapped_elements.Add(elements[i]);
                }
            }
            //calculate remapped coordinate for each gene    
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                remapped_elements[i].getGene().setRemappedCoordinate(i);
            }

            for (int i = 0; i <= remapped_elements.Count - 1; i++)
            {
                Gene gene_i = remapped_elements[i].getGene();
                if (gene_i.IsMirRna())
                {
                    int j = i - 1;
                    while (j >= 0 && j >= i - (gap + 1))
                    {
                        Gene gene_j = elements[j].getGene();
                        if (gene_i.hasHomologys() && gene_j.isIndirectHomologyWith(gene_i))
                        {

                            gene_j.remapTo(gene_i);
                           
                        }
                        j--;
                    }
                    j = i + 1;
                    while (j <= remapped_elements.Count - 1 && j <= i + (gap + 1))
                    {
                        Gene gene_j = elements[j].getGene();
                        if (gene_i.hasHomologys() && gene_j.isIndirectHomologyWith(gene_i))
                        {

                            gene_j.remapTo(gene_i);

                        }
                        j++;
                    }
                }                
            }

            for (int i = 0; i < remapped_elements.Count; i++)
            {
                if (remapped_elements[i].getGene().isRemapped())
                {
                    remapped_elements.RemoveAt(i);
                }
            }

            for (int i = 0; i < remapped_elements.Count; i++)
            {
                remapped_elements[i].getGene().setRemappedCoordinate(i);
            }
        }

        /*inverts the order of a section of this segment between the 2 integer arguments*/
        public void invertSection(int begin, int end)
        {
            List<Element> section = new List<Element>();
            if (begin < 0)
            {
                begin = 0;
            }
            if (end > remapped_elements.Count - 1)
            {
                end = remapped_elements.Count - 1;
            }
            for (int i = begin; i < end + 1; i++ )
            {
                section.Add(remapped_elements[i]);
            }
            section.Reverse();
            for (int i = begin, j = 0; i < remapped_elements.Count && j < section.Count; i++, j++)
            {
                remapped_elements[i] = section[j];
            }
        }
                
        
        //Set
        public void setColor(int r, int g, int b) { color = Color.FromArgb(r, g, b); }

        //Get
        //according to coordince
        public string getGeneName(int i) 
        {
            string erro = "Gene not  found!";
            if (i >= 0 && i < remapped_elements.Count) {                
                return remapped_elements[i].getGene().getID();                
            }
            return erro;
        }

        //Get Gene according to coordince
        public Gene getGene(int i) 
        {
            if (i >= 0 && i < remapped_elements.Count) {
                return remapped_elements[i].getGene();           
            } else {
                MessageBox.Show("Not such gene");
                return null;
            }
        }

        public List<Element> getRemappedElements() { return remapped_elements; }
        public int getRemappedElementsLength() { return remapped_elements.Count; }

        public int getNonGapElementsLength()
        {
            int num = 0;
            IEnumerator<Element> it = remapped_elements.GetEnumerator();
            while (it.MoveNext())
            {
               num++;
            }
            return num;
        }

        public int getElementsLength() { return elements.Count; }

        //const string getGeneName(int i);
        public string getListName() { return list_name; }

        public List<Element> getElements() { return elements; }

        public Element getElementByGeneID(string gene_name)
        {
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                if (remapped_elements[i].getGene().getID().Equals(gene_name))
                    return remapped_elements[i];
            }
            return null;
        }

        //Search in elements
        public Gene getGeneByID(string gene_name)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].getGene().getID().Equals(gene_name))
                    return elements[i].getGene();
            }
            return null;
        }

        public Gene getRemappedGeneByID(string gene_name)
        {
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                if (remapped_elements[i].getGene().getID().Equals(gene_name))
                    return remapped_elements[i].getGene();
            }
            return null;
        }

        public int getSize() { return remapped_elements.Count; }

        public int getCoordByGeneName(string id)
        {
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                if (remapped_elements[i].getGene().getID().Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public List<int> getMirCoordinate()
        {
            List<int> coordinates = new List<int>();
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                if (remapped_elements[i].getGene().IsMirRna())
                {
                    coordinates.Add(i);
                }
            }
            return coordinates;
        }

        public Gene getMirGene()
        {
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                if (remapped_elements[i].getGene().IsMirRna())
                {
                    return remapped_elements[i].getGene();
                }
            }
            return null;
        }

        public int getOrientation()
        {
            int positive = 0, negative = 0;
            for (int i = 0; i < remapped_elements.Count; i++)
            {
                if (remapped_elements[i].getOrientation() == 1)
                {
                    positive++;
                }
                else
                {
                    negative++;
                }
            }
            if (positive >= negative)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
        public Color getColor() { return color; }
    }
}
