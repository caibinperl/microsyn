namespace MirSyn
{
    partial class MicroSynForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MicroSynForm));
            this.msyOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMysMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syntenyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.generateMysStripButton = new System.Windows.Forms.ToolStripButton();
            this.dataFileButton = new System.Windows.Forms.ToolStripButton();
            this.closeDataButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.kstoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msyOpenFileDialog
            // 
            this.msyOpenFileDialog.FileName = "msyOpenFileDialog";
            this.msyOpenFileDialog.RestoreDirectory = true;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateMysMenuItem,
            this.dataFileMenuItem,
            this.closeDataMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.fileToolStripMenuItem.Text = "File (&F)";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // generateMysMenuItem
            // 
            this.generateMysMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("generateMysMenuItem.Image")));
            this.generateMysMenuItem.Name = "generateMysMenuItem";
            this.generateMysMenuItem.Size = new System.Drawing.Size(196, 22);
            this.generateMysMenuItem.Text = "Generate MSY Data (&G)";
            this.generateMysMenuItem.Click += new System.EventHandler(this.generateMysMenuItem_Click);
            // 
            // dataFileMenuItem
            // 
            this.dataFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dataFileMenuItem.Image")));
            this.dataFileMenuItem.Name = "dataFileMenuItem";
            this.dataFileMenuItem.Size = new System.Drawing.Size(196, 22);
            this.dataFileMenuItem.Text = "Load MSY Data (&L)";
            this.dataFileMenuItem.Click += new System.EventHandler(this.dataFileMenuItem_Click);
            // 
            // closeDataMenuItem
            // 
            this.closeDataMenuItem.Enabled = false;
            this.closeDataMenuItem.Image = global::MirSyn.Properties.Resources.close;
            this.closeDataMenuItem.Name = "closeDataMenuItem";
            this.closeDataMenuItem.Size = new System.Drawing.Size(196, 22);
            this.closeDataMenuItem.Text = "Close MSY Data (&C)";
            this.closeDataMenuItem.Click += new System.EventHandler(this.closeDataMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exitToolStripMenuItem.Text = "Exit (&E)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // syntenyToolStripMenuItem
            // 
            this.syntenyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runMenuItem});
            this.syntenyToolStripMenuItem.Name = "syntenyToolStripMenuItem";
            this.syntenyToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.syntenyToolStripMenuItem.Text = "Synteny (&S)";
            // 
            // runMenuItem
            // 
            this.runMenuItem.Enabled = false;
            this.runMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runMenuItem.Image")));
            this.runMenuItem.Name = "runMenuItem";
            this.runMenuItem.Size = new System.Drawing.Size(178, 22);
            this.runMenuItem.Text = "Detect Synteny (&R)";
            this.runMenuItem.Click += new System.EventHandler(this.runMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.syntenyToolStripMenuItem,
            this.ksToolStripMenuItem,
            this.helpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(358, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ksToolStripMenuItem
            // 
            this.ksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ksMenuItem});
            this.ksToolStripMenuItem.Name = "ksToolStripMenuItem";
            this.ksToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ksToolStripMenuItem.Text = "Ks (&K)";
            this.ksToolStripMenuItem.Click += new System.EventHandler(this.ksToolStripMenuItem_Click);
            // 
            // ksMenuItem
            // 
            this.ksMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ksMenuItem.Image")));
            this.ksMenuItem.Name = "ksMenuItem";
            this.ksMenuItem.Size = new System.Drawing.Size(286, 22);
            this.ksMenuItem.Text = "Caculate Synonymous Substitution (&S)";
            this.ksMenuItem.Click += new System.EventHandler(this.ksMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(65, 20);
            this.helpMenuItem.Text = "Help (&H)";
            this.helpMenuItem.Click += new System.EventHandler(this.helpMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateMysStripButton,
            this.dataFileButton,
            this.closeDataButton,
            this.toolStripButton3,
            this.kstoolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(358, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // generateMysStripButton
            // 
            this.generateMysStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.generateMysStripButton.Image = ((System.Drawing.Image)(resources.GetObject("generateMysStripButton.Image")));
            this.generateMysStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateMysStripButton.Name = "generateMysStripButton";
            this.generateMysStripButton.Size = new System.Drawing.Size(23, 22);
            this.generateMysStripButton.Text = "Generate MSY Data";
            this.generateMysStripButton.Click += new System.EventHandler(this.generateMysStripButton_Click);
            // 
            // dataFileButton
            // 
            this.dataFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dataFileButton.Image = ((System.Drawing.Image)(resources.GetObject("dataFileButton.Image")));
            this.dataFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dataFileButton.Name = "dataFileButton";
            this.dataFileButton.Size = new System.Drawing.Size(23, 22);
            this.dataFileButton.Text = "Load MSY Data";
            this.dataFileButton.Click += new System.EventHandler(this.dataFileButton_Click);
            // 
            // closeDataButton
            // 
            this.closeDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeDataButton.Enabled = false;
            this.closeDataButton.Image = global::MirSyn.Properties.Resources.close;
            this.closeDataButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeDataButton.Name = "closeDataButton";
            this.closeDataButton.Size = new System.Drawing.Size(23, 22);
            this.closeDataButton.Text = "Close MSY Data";
            this.closeDataButton.Click += new System.EventHandler(this.closeDataButton_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton3.Image = global::MirSyn.Properties.Resources.run;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Detect Synteny";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // kstoolStripButton
            // 
            this.kstoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.kstoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("kstoolStripButton.Image")));
            this.kstoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.kstoolStripButton.Name = "kstoolStripButton";
            this.kstoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.kstoolStripButton.Text = "Caculate Synonymous Substitution";
            this.kstoolStripButton.Click += new System.EventHandler(this.kstoolStripButton_Click);
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(23, 157);
            this.linkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(149, 12);
            this.linkLabel.TabIndex = 5;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "Go to MircroSyn web page";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusLabel.Location = new System.Drawing.Point(-3, 232);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(56, 14);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "Status:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.linkLabel);
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 178);
            this.panel1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(23, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "    Step 4: Ks -> Caculate Synonymous Substitution";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(22, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "    Step 3: Synteny -> Detect Synteny";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(21, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "    Step 2: File -> Load MSY Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(21, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "    Step 1: File -> Generate MSY File";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MicroSynForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 251);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MicroSynForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MicroSyn";
            this.Load += new System.EventHandler(this.MicroSynForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog msyOpenFileDialog;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syntenyToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem runMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ksMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton dataFileButton;
        private System.Windows.Forms.ToolStripButton closeDataButton;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.LinkLabel linkLabel;
        public System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem generateMysMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripButton generateMysStripButton;
        private System.Windows.Forms.ToolStripButton kstoolStripButton;
    }
}

