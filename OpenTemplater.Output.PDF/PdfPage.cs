using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using iTextSharp.text.pdf;
using OpenTemplater.Models;
using OpenTemplater.Output.PDF.Elements.Text;

namespace OpenTemplater.Output.PDF
{
    public class PdfPage
    {
        private PdfDocument _pdfDocument;
        private PdfContentByte _content;
        
        public PdfPage(PdfDocument document, PdfContentByte content)
        {
            _pdfDocument = document;
            _content = content;
        }

        /// <summary>
        /// Processes 
        /// </summary>
        /// <param name="pageTemplate"></param>
        /// <param name="itextContent"></param>
        public void Render(IElementContainer pageTemplate)
        {
            Console.WriteLine("Rendering element " + pageTemplate.Key);
            
            foreach (IPageElement bElement in pageTemplate.Contents.Elements.OrderBy(o => o.ZOrder))
            {
                switch (bElement.GetType().Name.ToLower())
                {
                    case "text":

                        PdfText pPdfText = new PdfText(_content, _pdfDocument, bElement);

                        pPdfText.Render();

                        break;

                    case "line": 
                        PdfLine line = new PdfLine(_content, _pdfDocument, bElement);

                        break;

                    case "image":

                        PdfImage image = new PdfImage(_content, _pdfDocument, bElement);
                        image.Render();
                        
                        break;

                    case "rectangle":

                        IElementContainer container = bElement as IElementContainer;
                        if (container != null)
                        {
                            if (container.Contents.Elements.Count() > 0)
                            {
                                Render(container);
                            }
                            else
                            {
                                PdfRectangle rectangle = new PdfRectangle(_content, _pdfDocument, bElement);
                                rectangle.Render();
                            }
                        }
                        break;
                }

            }
        }

    }
}
