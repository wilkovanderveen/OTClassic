using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf.draw;
using OpenTemplater.Presentation.PDF.Extensions.Drawing;

namespace OpenTemplater.Presentation.PDF.Elements.Text
{
    public class TextElement
    {
        private Models.Text.TextElement _bTextElement;
        private iTextSharp.text.Font _textFont;
        private iTextSharp.text.Color _color;
        private PdfDocument _pdfPdfDocument;
        private String _text;

        private TextElement(Font font, Models.Text.TextElement bTextElement, Color color)
        {
            ITextFont = font;
            ITextFont.Color = color;
            ITextFont.Size = bTextElement.FontSize.Points;
            Text = bTextElement.Text;
        }

        public Font ITextFont
        {
            get { return _textFont; }
            set { _textFont = value; }
        }

        public PdfDocument PdfPdfDocument
        {
            get { return _pdfPdfDocument; }
            set { _pdfPdfDocument = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Models.Text.TextElement BusinessObject
        {
            get { return _bTextElement; }
            set { _bTextElement = value; }
        }

        public static TextElement Parse(PdfDocument pdfDocument, Models.Text.TextElement bTextElement)
        {
            Font font = new Font(pdfDocument.Fonts[bTextElement.FontStyle.Font.Key + "_" + bTextElement.FontStyle.Key].Font);
            
            font.Size = bTextElement.FontSize.Points;
            iTextSharp.text.Color color = pdfDocument.Colors[bTextElement.Color.Key].RGBColor;
            return new TextElement(font, bTextElement, color);
        }

        public void Render(iTextSharp.text.Paragraph itextParagraph)
        {
            Chunk textChunk = new Chunk(Text, ITextFont);
            itextParagraph.Add(textChunk);

        }
    }
}