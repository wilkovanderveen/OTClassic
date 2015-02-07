using iTextSharp.text.pdf;
using OpenTemplater.Models;
using OpenTemplater.Presentation;

namespace OpenTemplater.Output.PDF
{
    public class PdfRectangle : BaseElement<Models.Rectangle>
    {
        
        public PdfRectangle(PdfContentByte content, PdfDocument pdfDocument, IPageElement rectangle) : base(content, pdfDocument, rectangle)
        {
        }


        public override void RenderObject()
        {
            PdfContent.SetColorStroke(Document.Colors[Element.BorderColor.Key].CMYKColor);

            if (Element.FillColor != null)
            {
                PdfContent.SetColorFill(Document.Colors[Element.FillColor.Key].CMYKColor);
            }


            if (Element.Roundness != null)
            {
                PdfContent.RoundRectangle(Element.LayoutContainer.Layout.Left.Points,
                                        Element.LayoutContainer.Layout.Bottom.Points,
                                        Element.LayoutContainer.Layout.Width.Points,
                                        Element.LayoutContainer.Layout.Height.Points,
                                        Element.Roundness.Points);
            }
            else
            {
                PdfContent.Rectangle(Element.LayoutContainer.Layout.Left.Points,
                                   Element.LayoutContainer.Layout.Bottom.Points,
                                   Element.LayoutContainer.Layout.Width.Points,
                                   Element.LayoutContainer.Layout.Height.Points);
            }

            if (Element.FillColor != null)
            {
                PdfContent.FillStroke();
            }
        }
    }
}
