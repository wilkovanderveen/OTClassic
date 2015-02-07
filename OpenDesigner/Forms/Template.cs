using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenDesigner.Forms
{
    public partial class Template : Form
    {
        public Template()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog(this);
            
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                CreateTemplate(saveFileDialog.FileName, txtTemplateName.Text, txtTemplateDescription.Text);
            }
            
        }

      

        private void CreateTemplate(string fileUri, string name, string description)
        {
            OpenTemplater.Models.Document document = OpenTemplater.Models.Document.Create(fileUri);

            document.Subject = description;
            document.Title = name;

            System.Xml.XmlDocument result = document.ToXml();
            result.Save(document.Uri.LocalPath);
        }
    }
}
