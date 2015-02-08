using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDesigner.Forms;
using OpenTemplater.Models;
using OpenTemplater.Models.Text;

namespace OpenDesigner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            System.Xml.XmlDocument xmlSettings = new System.Xml.XmlDocument();
            xmlSettings.Load("OpenDesigner.XmlSettings");

            TextFieldXMLPath.Text =
                xmlSettings.SelectSingleNode("/settings/setting[@name='XmlDocument']").Attributes["value"].Value;
            TextFieldXSLPath.Text =
               xmlSettings.SelectSingleNode("/settings/setting[@name='XslDocument']").Attributes["value"].Value;
        }

        private void ButtonXSLFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openXSLFile = new OpenFileDialog();
            openXSLFile.Filter = "XSL stylesheet file|*.xsl";
            openXSLFile.ShowDialog(this);

            if (!string.IsNullOrEmpty(openXSLFile.FileName))
            {
                TextFieldXSLPath.Text = openXSLFile.FileName;
            }
        }

        private void ButtonXMLFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openXMLFile = new OpenFileDialog();
            openXMLFile.Filter = "XML input file|*.xml";
            openXMLFile.ShowDialog(this);

            if (!string.IsNullOrEmpty(openXMLFile.FileName))
            {
                TextFieldXMLPath.Text = openXMLFile.FileName;
            }
        }

        private void ButtonTransform_Click(object sender, EventArgs e)
        {
            
            Document bDocument = new Document();
            try
            {
                bDocument.Load(TextFieldXMLPath.Text, TextFieldXSLPath.Text, Application.StartupPath + "/document.xsd");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error while transforming via XSL");
                return;
            }

            SaveFileDialog savePDFFile = new SaveFileDialog();
            savePDFFile.Filter = "PDF File|*.pdf";
            savePDFFile.ShowDialog(this);

            if (!string.IsNullOrEmpty(savePDFFile.FileName))
            {
                Render renderForm = new Render();
                renderForm.RenderPdf(this, TextFieldXSLPath.Text, TextFieldXMLPath.Text, savePDFFile.FileName, bDocument);
            }
        }

        private void FillTree(Document bDocument)
        {
            TreeNode documentTreeNode = new TreeNode();
            documentTreeNode.Tag = bDocument;
            documentTreeNode.Text = "Document";
            treeDocument.Nodes.Clear();
            treeDocument.Nodes.Add(documentTreeNode);



            // Process fonts
            if (bDocument.Fonts.Count > 0)
            {
                foreach (OpenTemplater.Models.Typography.Font bFont in bDocument.Fonts)
                {
                    TreeNode fontNode = new TreeNode();
                    fontNode.Tag = bFont;
                    fontNode.Text = "Font " + bFont.Key;

                    documentTreeNode.Nodes.Add(fontNode);
                    foreach (OpenTemplater.Models.Typography.FontStyle bFontStyle in bFont.Styles)
                    {
                        TreeNode fontStyleNode = new TreeNode();
                        fontStyleNode.Tag = bFontStyle;
                        fontStyleNode.Text = "Style " + bFontStyle.Key;
                        fontNode.Nodes.Add(fontStyleNode);
                    }

                }
            }

            // Process colors
            if (bDocument.Colors.Count > 0)
            {
                foreach (OpenTemplater.Models.Typography.Color bColor in bDocument.Colors)
                {
                    TreeNode colorNode = new TreeNode();
                    colorNode.Tag = bColor;
                    colorNode.Text = "Color " + bColor.Key;

                    documentTreeNode.Nodes.Add(colorNode);
                }
            }

            // Process document
            if (bDocument.Pages.Count > 0)
            {
                TreeNode pagesTreeNode = documentTreeNode.Nodes.Add("Pages");
                foreach (OpenTemplater.Models.Page bPage in bDocument.Pages)
                {
                    TreeNode pageNode = new TreeNode();
                    pageNode.Tag = bPage;
                    pageNode.Text = "Page " + bPage.Key;

                    pagesTreeNode.Nodes.Add(pageNode);

                    foreach (OpenTemplater.Models.IPageElement bElement in bPage.Contents.Elements)
                    {
                        TreeNode elementNode = new TreeNode();
                        elementNode.Tag = bElement;
                        elementNode.Text = bElement.GetType().Name + " " + bElement.Key;

                        pageNode.Nodes.Add(elementNode);

                        TreeNode elementLayoutNode = new TreeNode();
                        elementLayoutNode.Tag = bElement.LayoutContainer;
                        elementLayoutNode.Text = "Layout";

                        elementNode.Nodes.Add(elementLayoutNode);

                        if (bElement is Text)
                        {
                            TreeNode paragraphsNode = elementNode.Nodes.Add("Paragraphs");
                            foreach (Paragraph bParagraph in ((OpenTemplater.Models.Text.Text)bElement).Paragraphs)
                            {
                                TreeNode paragraphNode = new TreeNode();
                                paragraphNode.Tag = bParagraph;
                                paragraphNode.Text = "Paragraph";

                                paragraphsNode.Nodes.Add(paragraphNode);

                                foreach (TextElement bTextElement in bParagraph.TextElements)
                                {
                                    TreeNode textElementNode = new TreeNode();
                                    textElementNode.Tag = bTextElement;
                                    textElementNode.Text = "TextElement";

                                    paragraphNode.Nodes.Add(textElementNode);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TreeNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
             
        }

        private void ItemProperties_CollectionChanging(Object sender, EventArgs e)
        {
            
        }

        private void MenuMainFileNewProject_Click(object sender, EventArgs e)
        {
            Forms.Template templateForm = new Template();
            templateForm.ShowDialog(this);
        }
    }
}
