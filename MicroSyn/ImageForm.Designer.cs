namespace MirSyn
{
    partial class ImageForm
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
            this.circlePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.circlePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // circlePictureBox
            // 
            this.circlePictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.circlePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.circlePictureBox.Location = new System.Drawing.Point(0, 0);
            this.circlePictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.circlePictureBox.Name = "circlePictureBox";
            this.circlePictureBox.Size = new System.Drawing.Size(470, 394);
            this.circlePictureBox.TabIndex = 3;
            this.circlePictureBox.TabStop = false;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 394);
            this.Controls.Add(this.circlePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "imageForm";
            ((System.ComponentModel.ISupportInitialize)(this.circlePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox circlePictureBox;
    }
}