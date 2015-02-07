namespace OpenDesigner
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuMain = new System.Windows.Forms.MenuStrip();
            this.MenuMainFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMainFileNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextFieldXMLPath = new System.Windows.Forms.TextBox();
            this.ButtonXMLFile = new System.Windows.Forms.Button();
            this.ButtonXSLFile = new System.Windows.Forms.Button();
            this.TextFieldXSLPath = new System.Windows.Forms.TextBox();
            this.ButtonTransform = new System.Windows.Forms.Button();
            this.treeDocument = new System.Windows.Forms.TreeView();
            this.PageImageBox = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.MenuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageImageBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuMain
            // 
            this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMainFile,
            this.editToolStripMenuItem});
            this.MenuMain.Location = new System.Drawing.Point(0, 0);
            this.MenuMain.Name = "MenuMain";
            this.MenuMain.Size = new System.Drawing.Size(1146, 24);
            this.MenuMain.TabIndex = 0;
            this.MenuMain.Text = "Main";
            // 
            // MenuMainFile
            // 
            this.MenuMainFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMainFileNewProject});
            this.MenuMainFile.Name = "MenuMainFile";
            this.MenuMainFile.Size = new System.Drawing.Size(37, 20);
            this.MenuMainFile.Text = "&File";
            // 
            // MenuMainFileNewProject
            // 
            this.MenuMainFileNewProject.Name = "MenuMainFileNewProject";
            this.MenuMainFileNewProject.Size = new System.Drawing.Size(138, 22);
            this.MenuMainFileNewProject.Text = "&New project";
            this.MenuMainFileNewProject.Click += new System.EventHandler(this.MenuMainFileNewProject_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // TextFieldXMLPath
            // 
            this.TextFieldXMLPath.Location = new System.Drawing.Point(7, 103);
            this.TextFieldXMLPath.Name = "TextFieldXMLPath";
            this.TextFieldXMLPath.Size = new System.Drawing.Size(303, 20);
            this.TextFieldXMLPath.TabIndex = 1;
            // 
            // ButtonXMLFile
            // 
            this.ButtonXMLFile.Location = new System.Drawing.Point(316, 100);
            this.ButtonXMLFile.Name = "ButtonXMLFile";
            this.ButtonXMLFile.Size = new System.Drawing.Size(75, 23);
            this.ButtonXMLFile.TabIndex = 2;
            this.ButtonXMLFile.Text = "XML File";
            this.ButtonXMLFile.UseVisualStyleBackColor = true;
            this.ButtonXMLFile.Click += new System.EventHandler(this.ButtonXMLFile_Click);
            // 
            // ButtonXSLFile
            // 
            this.ButtonXSLFile.Location = new System.Drawing.Point(316, 126);
            this.ButtonXSLFile.Name = "ButtonXSLFile";
            this.ButtonXSLFile.Size = new System.Drawing.Size(75, 23);
            this.ButtonXSLFile.TabIndex = 4;
            this.ButtonXSLFile.Text = "XSL File";
            this.ButtonXSLFile.UseVisualStyleBackColor = true;
            this.ButtonXSLFile.Click += new System.EventHandler(this.ButtonXSLFile_Click);
            // 
            // TextFieldXSLPath
            // 
            this.TextFieldXSLPath.Location = new System.Drawing.Point(7, 129);
            this.TextFieldXSLPath.Name = "TextFieldXSLPath";
            this.TextFieldXSLPath.Size = new System.Drawing.Size(303, 20);
            this.TextFieldXSLPath.TabIndex = 3;
            // 
            // ButtonTransform
            // 
            this.ButtonTransform.Location = new System.Drawing.Point(504, 501);
            this.ButtonTransform.Name = "ButtonTransform";
            this.ButtonTransform.Size = new System.Drawing.Size(99, 23);
            this.ButtonTransform.TabIndex = 4;
            this.ButtonTransform.Text = "Generate PDF";
            this.ButtonTransform.UseVisualStyleBackColor = true;
            this.ButtonTransform.Click += new System.EventHandler(this.ButtonTransform_Click);
            // 
            // treeDocument
            // 
            this.treeDocument.Location = new System.Drawing.Point(7, 176);
            this.treeDocument.Name = "treeDocument";
            this.treeDocument.Size = new System.Drawing.Size(596, 320);
            this.treeDocument.TabIndex = 5;
            this.treeDocument.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeNodeClick);
            // 
            // PageImageBox
            // 
            this.PageImageBox.Location = new System.Drawing.Point(609, 99);
            this.PageImageBox.Name = "PageImageBox";
            this.PageImageBox.Size = new System.Drawing.Size(507, 596);
            this.PageImageBox.TabIndex = 7;
            this.PageImageBox.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1146, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "btnAddNewImage";
            this.toolStripButton1.ToolTipText = "Add new image";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 707);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.PageImageBox);
            this.Controls.Add(this.treeDocument);
            this.Controls.Add(this.ButtonTransform);
            this.Controls.Add(this.ButtonXSLFile);
            this.Controls.Add(this.TextFieldXSLPath);
            this.Controls.Add(this.ButtonXMLFile);
            this.Controls.Add(this.TextFieldXMLPath);
            this.Controls.Add(this.MenuMain);
            this.MainMenuStrip = this.MenuMain;
            this.Name = "MainForm";
            this.Text = "OpenDesigner";
            this.MenuMain.ResumeLayout(false);
            this.MenuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageImageBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuMain;
        private System.Windows.Forms.ToolStripMenuItem MenuMainFile;
        private System.Windows.Forms.TextBox TextFieldXMLPath;
        private System.Windows.Forms.Button ButtonXMLFile;
        private System.Windows.Forms.Button ButtonXSLFile;
        private System.Windows.Forms.TextBox TextFieldXSLPath;
        private System.Windows.Forms.Button ButtonTransform;
        private System.Windows.Forms.TreeView treeDocument;
        private System.Windows.Forms.PictureBox PageImageBox;
        private System.Windows.Forms.ToolStripMenuItem MenuMainFileNewProject;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}

