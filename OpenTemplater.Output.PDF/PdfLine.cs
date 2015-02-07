using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using OpenTemplater.Models;
using OpenTemplater.Presentation;

namespace OpenTemplater.Output.PDF
{
    public class PdfLine : BaseElement<Models.Line>
    {
        public PdfLine(PdfContentByte content, PdfDocument document, IPageElement line) : base(content, document, line) 
        {}

        public override void RenderObject()
        {
            PdfContent.MoveTo(Element.LayoutContainer.Layout.Left.Points,
                                            Element.LayoutContainer.Layout.Bottom.Points);
            PdfContent.SetLineWidth(Element.Width.Points);
            PdfContent.SetColorStroke(Document.Colors[Element.Color.Key].CMYKColor);
            PdfContent.LineTo(Element.LayoutContainer.Layout.Right.Points,
                                Element.LayoutContainer.Layout.Top.Points);
            PdfContent.Stroke();
        }
    }
}
