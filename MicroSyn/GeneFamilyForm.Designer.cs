namespace MirSyn
{
    partial class GeneFamilyForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listFileButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.mirRegionLenTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.listOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.speciesTextBox = new System.Windows.Forms.TextBox();
            this.familyFileBbutton = new System.Windows.Forms.Button();
            this.familyOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Family Gene File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Genome Gene List File:";
            // 
            // listFileButton
            // 
            this.listFileButton.Location = new System.Drawing.Point(224, 128);
            this.listFileButton.Name = "listFileButton";
            this.listFileButton.Size = new System.Drawing.Size(75, 23);
            this.listFileButton.TabIndex = 3;
            this.listFileButton.Text = "Browse";
            this.listFileButton.UseVisualStyleBackColor = true;
            this.listFileButton.Click += new System.EventHandler(this.listFileButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "The number of neighboring genes:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // mirRegionLenTextBox
            // 
            this.mirRegionLenTextBox.Location = new System.Drawing.Point(224, 170);
            this.mirRegionLenTextBox.Name = "mirRegionLenTextBox";
            this.mirRegionLenTextBox.Size = new System.Drawing.Size(75, 21);
            this.mirRegionLenTextBox.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(26, 206);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(64, 21);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Enter";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // listOpenFileDialog
            // 
            this.listOpenFileDialog.RestoreDirectory = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Input species name:";
            // 
            // speciesTextBox
            // 
            this.speciesTextBox.Location = new System.Drawing.Point(223, 35);
            this.speciesTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.speciesTextBox.Name = "speciesTextBox";
            this.speciesTextBox.Size = new System.Drawing.Size(76, 21);
            this.speciesTextBox.TabIndex = 8;
            // 
            // familyFileBbutton
            // 
            this.familyFileBbutton.Location = new System.Drawing.Point(224, 77);
            this.familyFileBbutton.Name = "familyFileBbutton";
            this.familyFileBbutton.Size = new System.Drawing.Size(75, 23);
            this.familyFileBbutton.TabIndex = 9;
            this.familyFileBbutton.Text = "Browse";
            this.familyFileBbutton.UseVisualStyleBackColor = true;
            this.familyFileBbutton.Click += new System.EventHandler(this.familyFileBbutton_Click);
            // 
            // GeneFamilyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 241);
            this.Controls.Add(this.familyFileBbutton);
            this.Controls.Add(this.speciesTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.mirRegionLenTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GeneFamilyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gene family and list";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button listFileButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mirRegionLenTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.OpenFileDialog listOpenFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox speciesTextBox;
        private System.Windows.Forms.Button familyFileBbutton;
        private System.Windows.Forms.OpenFileDialog familyOpenFileDialog;
    }
}