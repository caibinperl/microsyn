namespace MirSyn
{
    partial class OutputForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Genes in faimily");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Neighboring genes");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Input data", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Synteny");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Homologys");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Result data", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsTXTMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.displayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveEmfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.imageSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveXmlFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(641, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsTXTMenuItem});
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(47, 21);
            this.saveMenuItem.Text = "Save";
            // 
            // saveAsTXTMenuItem
            // 
            this.saveAsTXTMenuItem.Name = "saveAsTXTMenuItem";
            this.saveAsTXTMenuItem.Size = new System.Drawing.Size(167, 22);
            this.saveAsTXTMenuItem.Text = "Save as TXT file";
            this.saveAsTXTMenuItem.Click += new System.EventHandler(this.saveAsTXTMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayMenuItem,
            this.fontsToolStripMenuItem,
            this.optionsMenuItem,
            this.toolStripSeparator1,
            this.saveEmfMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(57, 21);
            this.toolStripMenuItem1.Text = "Image";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // displayMenuItem
            // 
            this.displayMenuItem.Name = "displayMenuItem";
            this.displayMenuItem.Size = new System.Drawing.Size(122, 22);
            this.displayMenuItem.Text = "Display";
            this.displayMenuItem.Click += new System.EventHandler(this.displayMenuItem_Click);
            // 
            // fontsToolStripMenuItem
            // 
            this.fontsToolStripMenuItem.Name = "fontsToolStripMenuItem";
            this.fontsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.fontsToolStripMenuItem.Text = "Fonts";
            this.fontsToolStripMenuItem.Click += new System.EventHandler(this.fontsMenuItem_Click);
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(122, 22);
            this.optionsMenuItem.Text = "Options";
            this.optionsMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // saveEmfMenuItem
            // 
            this.saveEmfMenuItem.Name = "saveEmfMenuItem";
            this.saveEmfMenuItem.Size = new System.Drawing.Size(122, 22);
            this.saveEmfMenuItem.Text = "Save";
            this.saveEmfMenuItem.Visible = false;
            this.saveEmfMenuItem.Click += new System.EventHandler(this.saveEmfMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 45);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node2";
            treeNode1.Text = "Genes in faimily";
            treeNode2.Name = "Node3";
            treeNode2.Text = "Neighboring genes";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Input data";
            treeNode4.Name = "Node4";
            treeNode4.Text = "Synteny";
            treeNode5.Name = "Node6";
            treeNode5.Text = "Homologys";
            treeNode6.Name = "Node1";
            treeNode6.Text = "Result data";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6});
            this.treeView1.Size = new System.Drawing.Size(164, 120);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(182, 45);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(450, 442);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 499);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "OutputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OutputForm";
            this.Load += new System.EventHandler(this.OutputForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem displayMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveEmfMenuItem;
        private System.Windows.Forms.SaveFileDialog imageSaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveAsTXTMenuItem;
        private System.Windows.Forms.SaveFileDialog saveXmlFileDialog;

    }
}