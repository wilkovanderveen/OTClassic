using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Output.PDF
{
    public class PdfRenderEngine : IRenderEngine
    {
        //public void RenderBitmap(string xmlUri, string xslUri, string outputFile)
        //{
        //    OpenTemplater.Models.Document bDocument = new OpenTemplater.Models.Document();
        //    bDocument.Load(xmlUri, xslUri);

        //    Bitmap.Document bitmapDocument = new OpenTemplater.Presentation.Bitmap.Document(bDocument,
        //                                                                                    outputFile + ".bmp");
        //}
        

        /// <summary>
        /// Renders a PDF file.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="outputFile"></param>
        public void Render(string uri, string outputFile)
        {
            OpenTemplater.Models.Document bDocument = new OpenTemplater.Models.Document();
            bDocument.Load(uri);

           PdfDocument pdfPdfDocument = new PdfDocument(bDocument, outputFile);
        }

        public void Render(string xmlUri, string xslUri, string outputFile)
        {
            OpenTemplater.Models.Document bDocument = new OpenTemplater.Models.Document();
            bDocument.Load(xmlUri, xslUri);

            PdfDocument pdfPdfDocument = new PdfDocument(bDocument, outputFile);
        }

        public void Render(Models.Document document, string outputFile)
        {
            PdfDocument pdfPdfDocument = new PdfDocument(document, outputFile);
        }
    }
}