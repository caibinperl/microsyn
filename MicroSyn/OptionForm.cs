using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MirSyn
{
    public partial class OptionForm : Form
    {        
        ImageData data;
        public OptionForm(ImageData data)
        {
            InitializeComponent();
            this.data = data;
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            //this.data.setWidth((int)widthNumericUpDown.Value);
            //this.data.setHeight((int)heightNumericUpDown.Value);
            this.data.setRadius((int)radiusNumericUpDown.Value);
            this.data.setInterval((int)intervalNumericUpDown.Value);
            this.data.setNodeHeight((int)geneWidthNumericUpDown.Value);
            this.data.setCircleData();
            new ImageForm(this.data).Show();
            this.Dispose();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
