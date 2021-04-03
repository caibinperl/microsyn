namespace MirSyn
{
    partial class RunForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.probCutoffTextBox = new System.Windows.Forms.TextBox();
            this.homologyPointsTextBox = new System.Windows.Forms.TextBox();
            this.tandemGapTextBox = new System.Windows.Forms.TextBox();
            this.gapSizeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.abortButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.probCutoffTextBox);
            this.groupBox1.Controls.Add(this.homologyPointsTextBox);
            this.groupBox1.Controls.Add(this.tandemGapTextBox);
            this.groupBox1.Controls.Add(this.gapSizeTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set the parameters ";
            // 
            // probCutoffTextBox
            // 
            this.probCutoffTextBox.Location = new System.Drawing.Point(181, 105);
            this.probCutoffTextBox.Name = "probCutoffTextBox";
            this.probCutoffTextBox.Size = new System.Drawing.Size(58, 21);
            this.probCutoffTextBox.TabIndex = 7;
            this.probCutoffTextBox.Text = "0.01";
            // 
            // homologyPointsTextBox
            // 
            this.homologyPointsTextBox.Location = new System.Drawing.Point(181, 78);
            this.homologyPointsTextBox.Name = "homologyPointsTextBox";
            this.homologyPointsTextBox.Size = new System.Drawing.Size(58, 21);
            this.homologyPointsTextBox.TabIndex = 6;
            this.homologyPointsTextBox.Text = "3";
            // 
            // tandemGapTextBox
            // 
            this.tandemGapTextBox.Location = new System.Drawing.Point(181, 53);
            this.tandemGapTextBox.Name = "tandemGapTextBox";
            this.tandemGapTextBox.Size = new System.Drawing.Size(58, 21);
            this.tandemGapTextBox.TabIndex = 5;
            this.tandemGapTextBox.Text = "2";
            // 
            // gapSizeTextBox
            // 
            this.gapSizeTextBox.Location = new System.Drawing.Point(181, 16);
            this.gapSizeTextBox.Name = "gapSizeTextBox";
            this.gapSizeTextBox.Size = new System.Drawing.Size(58, 21);
            this.gapSizeTextBox.TabIndex = 4;
            this.gapSizeTextBox.Text = "30";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Expected Value:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Homologous pairs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tandem gap size:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Window size:";
            // 
            // runButton
            // 
            this.runButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.runButton.Location = new System.Drawing.Point(182, 187);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(62, 23);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Detect";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(19, 230);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(47, 12);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "Status:";
            // 
            // abortButton
            // 
            this.abortButton.Location = new System.Drawing.Point(34, 187);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(57, 23);
            this.abortButton.TabIndex = 5;
            this.abortButton.Text = "Abort";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 254);
            this.Controls.Add(this.abortButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RunForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parameters setting";
            this.Load += new System.EventHandler(this.runForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox probCutoffTextBox;
        private System.Windows.Forms.TextBox homologyPointsTextBox;
        private System.Windows.Forms.TextBox tandemGapTextBox;
        private System.Windows.Forms.TextBox gapSizeTextBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button abortButton;
    }
}