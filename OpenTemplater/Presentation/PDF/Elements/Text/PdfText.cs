using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OpenTemplater.Models.Layout;

namespace OpenTemplater.Presentation.PDF.Elements.Text
{
    public class PdfText
    {
        private List<Textline> _textlines = new List<Textline>();
        private Models.Layout.LayoutContainer _objectLayoutContainer;

        private PdfText()
        {
        }

        /// <summary>
        /// Adds a textline to this text.
        /// </summary>
        /// <param name="textline"></param>
        public void Add(Textline textline)
        {
            _textlines.Add(textline);
        }

        public LayoutContainer ObjectLayoutContainer
        {
            get { return _objectLayoutContainer; }
            set { _objectLayoutContainer = value; }
        }

        public void Render(PdfContentByte itextContent)
        {
            iTextSharp.text.pdf.ColumnText itextText = new iTextSharp.text.pdf.ColumnText(itextContent);
            itextText.SetSimpleColumn(ObjectLayoutContainer.Layout.Left.Points, ObjectLayoutContainer.Layout.Top.Points,
                                      ObjectLayoutContainer.Layout.Right.Points,
                                      ObjectLayoutContainer.Layout.Bottom.Points);

            Paragraph pdfContentParagraph = new Paragraph();
            itextText.AddElement(pdfContentParagraph);

            if (_textlines.Count > 0)
            {
                Models.Text.Paragraph currentParagraph = _textlines.First().ParagraphModel;
                pdfContentParagraph.Leading = currentParagraph.Leading.Points;
                pdfContentParagraph.Alignment = (int) currentParagraph.Alignment;
                foreach (Textline textline in _textlines)
                {
                    if (currentParagraph != textline.ParagraphModel)
                    {
                        pdfContentParagraph = new Paragraph(textline.ParagraphModel.Leading.Points);
                        pdfContentParagraph.Alignment = (int)textline.ParagraphModel.Alignment;
                        itextText.AddElement(pdfContentParagraph);
                    }
                    textline.Render(pdfContentParagraph);
                    currentParagraph = textline.ParagraphModel;
                }
                itextText.Go();
            }
        }

        /// <summary>
        /// Parses an OpenTemplater Text model to a PDF text model.
        /// </summary>
        /// <param name="pdfDocument"></param>
        /// <param name="bText"></param>
        /// <returns></returns>
        public static PdfText Parse(PdfDocument pdfDocument, Models.Text.Text bText)
        {
            PdfText pPdfText = new PdfText();
            pPdfText.ObjectLayoutContainer = bText.LayoutContainer;

            foreach (Models.Text.Textline bTextline in bText.Textlines)
            {
                Textline textline = Textline.Parse(pdfDocument, bTextline);
                pPdfText.Add(textline);
            }

            return pPdfText;
        }
    }
}