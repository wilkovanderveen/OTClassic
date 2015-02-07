using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenTemplater.Models;
using OpenTemplater.Output.PDF;

namespace OpenDesigner.Forms
{
    public partial class Render : Form
    {
        public Render()
        {
            InitializeComponent();
        }

        private void Render_Load(object sender, EventArgs e)
        {
            TextWriter textwriter = new TextBoxStreamWriter(this.textOutput);
            Console.SetOut(textwriter);
        }

        public void EnableCloseButton()
        {
            btnClose.Enabled = true;
        }



        public void RenderPdf(IWin32Window owner, string xslPath, string xmlPath, string pdfFile, Document bDocument)
        {
            this.Show(owner);

            PdfRenderEngine re = new PdfRenderEngine();
            if (xslPath != "")
            {
                try
                {
                    re.Render(bDocument, pdfFile);

                }
                catch (IOException ioException)
                {
                    MessageBox.Show(ioException.Message);
                }

                //re.RenderBitmap(TextFieldXMLPath.Text, TextFieldXSLPath.Text, savePDFFile.FileName);
                //PageImageBox.Image = System.Drawing.Image.FromFile(savePDFFile.FileName + ".bmp");
            }
            else
            {
                re.Render(xmlPath, pdfFile);
            }

            EnableCloseButton();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
            this.Close();
        }
    }
    
    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
           
                base.Write(value);
                //_output.AppendText(value.ToString()); // When character data is written, append it to the text box.
            
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
