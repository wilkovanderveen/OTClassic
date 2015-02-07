using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using iTextSharp.text.pdf;
using OpenTemplater.Models;
using OpenTemplater.Presentation;

namespace OpenTemplater.Output.PDF
{
    public class PdfImage : BaseElement<Models.Image>
    {
        
        public PdfImage(PdfContentByte content, PdfDocument document, IPageElement image) : base(content, document, image)
        {
        }

        public override void RenderObject()
        {
            try
            {
                iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(Element.Uri);
                pdfImage.ScaleAbsolute(
                    Element.LayoutContainer.Layout.Right.Points - Element.LayoutContainer.Layout.Left.Points,
                    Element.LayoutContainer.Layout.Top.Points - Element.LayoutContainer.Layout.Bottom.Points);

                pdfImage.SetAbsolutePosition(Element.LayoutContainer.Layout.Left.Points,
                                           Element.LayoutContainer.Layout.Bottom.Points);
                PdfContent.AddImage(pdfImage);
            }
            catch (WebException wex)
            {
                throw;

            }
        }
    }
}
