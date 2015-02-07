using System.Collections.Generic;
using System.Text;
using iTextSharp.text;

namespace OpenTemplater.Output.PDF.Elements.Text
{
    public class Textline : IEnumerable<TextElement>
    {
        private List<TextElement> _textElements;
        private float _leading;
        private int _alignment;
        private bool _isLastlineOfParagraph;
        private Models.Text.Paragraph _paragraphModel;
        
        public Models.Text.Paragraph ParagraphModel
        {
            get { return _paragraphModel; }
        }

        public bool IsLastLineOfParagraph
        {
            get { return _isLastlineOfParagraph; }
        }

        public Textline()
        {
            _textElements = new List<TextElement>();
        }

        private Textline(PdfDocument pdfDocument, Models.Text.Textline bTextline)
        {
            _textElements = new List<TextElement>();
            _leading = bTextline.Height.Points;
            _alignment = (int) bTextline.Alignment;
            _isLastlineOfParagraph = bTextline.EndsParagraph;
            _paragraphModel = bTextline.Paragraph;

            foreach (Models.Text.TextElement bTextElement in bTextline)
            {
                _textElements.Add(TextElement.Parse(pdfDocument, bTextElement));
            }
        }

        public void Add(TextElement textElement)
        {
            _textElements.Add(textElement);
        }

        /// <summary>
        /// Parse a textline from a business object to a presentationlayer object.
        /// </summary>
      
        /// <param name="bTextline">Textline to be parsed into a presentation layer object.</param>
        /// <returns></returns>
        public static Textline Parse(PdfDocument pdfDocument, Models.Text.Textline bTextline)
        {
            return new Textline(pdfDocument, bTextline);
        }

        public IEnumerator<TextElement> GetEnumerator()
        {
            return _textElements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _textElements.GetEnumerator();
        }

        /// <summary>
        /// Renders a textline.
        /// </summary>
        /// <param name="paragraph"></param>
        public void Render(Paragraph paragraph)
        {
            if (_textElements.Count > 0)
            {
                foreach (TextElement te in _textElements)
                {
                    te.Render(paragraph);
                }
            }
            else
            {
                paragraph.Add(new Chunk(" "));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
          foreach(TextElement te in _textElements)
          {
              sb.Append(te.Text);
          }
            return sb.ToString();
        }
    }
}