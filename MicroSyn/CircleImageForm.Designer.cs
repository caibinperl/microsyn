namespace MirSyn
{
    partial class CircleImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsEMFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.circlePictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circlePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(942, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageOutMenuItem,
            this.saveAsEMFToolStripMenuItem});
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            // 
            // imageOutMenuItem
            // 
            this.imageOutMenuItem.Name = "imageOutMenuItem";
            this.imageOutMenuItem.Size = new System.Drawing.Size(142, 22);
            this.imageOutMenuItem.Text = "Save as  EMF";
            this.imageOutMenuItem.Click += new System.EventHandler(this.imageOutMenuItem_Click);
            // 
            // saveAsEMFToolStripMenuItem
            // 
            this.saveAsEMFToolStripMenuItem.Name = "saveAsEMFToolStripMenuItem";
            this.saveAsEMFToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.saveAsEMFToolStripMenuItem.Text = "Save as PNG";
            // 
            // imageSaveFileDialog
            // 
            this.imageSaveFileDialog.RestoreDirectory = true;
            // 
            // circlePictureBox
            // 
            this.circlePictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.circlePictureBox.Location = new System.Drawing.Point(0, 25);
            this.circlePictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.circlePictureBox.Name = "circlePictureBox";
            this.circlePictureBox.Size = new System.Drawing.Size(950, 700);
            this.circlePictureBox.TabIndex = 2;
            this.circlePictureBox.TabStop = false;
            // 
            // CircleImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 721);
            this.Controls.Add(this.circlePictureBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CircleImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CircleImageForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circlePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageOutMenuItem;
        private System.Windows.Forms.SaveFileDialog imageSaveFileDialog;
        private System.Windows.Forms.PictureBox circlePictureBox;
        private System.Windows.Forms.ToolStripMenuItem saveAsEMFToolStripMenuItem;
    }
}