using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OpenTemplater.Models;
using OpenTemplater.Models.Layout;
using OpenTemplater.Output.PDF.Elements.Text;

namespace OpenTemplater.Output.PDF
{
    public class PdfText : BaseElement<Models.Text.Text>
    {
        private List<Textline> _textlines = new List<Textline>();
        private Models.Layout.LayoutContainer _objectLayoutContainer;
        
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

        public override void RenderObject()
        {
            ColumnText itextText = new ColumnText(PdfContent);
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
                        pdfContentParagraph = new Paragraph(textline.ParagraphModel.Leading.Points)
                                                  {
                                                      Alignment = (int) textline.ParagraphModel.Alignment
                                                  };
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
        public PdfText(PdfContentByte content, PdfDocument pdfDocument, IPageElement bText)
            : base(content, pdfDocument, bText)
        {
           
            ObjectLayoutContainer = Element.LayoutContainer;
            ParseTextlines();

        }

        private void ParseTextlines()
        {
            foreach (Models.Text.Textline bTextline in Element.Textlines)
            {
                Textline textline = Textline.Parse(Document, bTextline);
                Add(textline);
            }
        }
    }
}