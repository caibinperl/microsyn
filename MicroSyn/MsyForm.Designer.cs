namespace MirSyn
{
    partial class MsyForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.geneListButton = new System.Windows.Forms.Button();
            this.seqFileButton = new System.Windows.Forms.Button();
            this.msyFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.seq0penFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.msySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(82, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Generate MSY File";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.geneListButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 98);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step 1:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(299, 36);
            this.label4.TabIndex = 1;
            this.label4.Text = "*Every time only input data from the same species\r\nClick the above button again i" +
                "f you have \r\nthe genes and list from another species";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // geneListButton
            // 
            this.geneListButton.Location = new System.Drawing.Point(9, 20);
            this.geneListButton.Name = "geneListButton";
            this.geneListButton.Size = new System.Drawing.Size(318, 23);
            this.geneListButton.TabIndex = 0;
            this.geneListButton.Text = "Input gene names and gene list file from a species";
            this.geneListButton.UseVisualStyleBackColor = true;
            this.geneListButton.Click += new System.EventHandler(this.geneListButton_Click);
            // 
            // seqFileButton
            // 
            this.seqFileButton.Location = new System.Drawing.Point(86, 168);
            this.seqFileButton.Name = "seqFileButton";
            this.seqFileButton.Size = new System.Drawing.Size(193, 23);
            this.seqFileButton.TabIndex = 2;
            this.seqFileButton.Text = "Select gene sequence files";
            this.seqFileButton.UseVisualStyleBackColor = true;
            this.seqFileButton.Click += new System.EventHandler(this.seqFileButton_Click);
            // 
            // msyFileButton
            // 
            this.msyFileButton.Location = new System.Drawing.Point(84, 215);
            this.msyFileButton.Name = "msyFileButton";
            this.msyFileButton.Size = new System.Drawing.Size(126, 23);
            this.msyFileButton.TabIndex = 3;
            this.msyFileButton.Text = "Save MSY file";
            this.msyFileButton.UseVisualStyleBackColor = true;
            this.msyFileButton.Click += new System.EventHandler(this.msyFileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(31, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Step 2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(33, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Step 3:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(255, 245);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // seq0penFileDialog
            // 
            this.seq0penFileDialog.FileName = "openFileDialog1";
            this.seq0penFileDialog.RestoreDirectory = true;
            // 
            // msySaveFileDialog
            // 
            this.msySaveFileDialog.RestoreDirectory = true;
            // 
            // MsyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 280);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.msyFileButton);
            this.Controls.Add(this.seqFileButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MsyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate MSY file";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button geneListButton;
        private System.Windows.Forms.Button seqFileButton;
        private System.Windows.Forms.Button msyFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog seq0penFileDialog;
        private System.Windows.Forms.SaveFileDialog msySaveFileDialog;
    }
}