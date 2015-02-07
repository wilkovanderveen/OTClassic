using iTextSharp.text;

namespace OpenTemplater.Output.PDF.Elements.Text
{
    public class TextElement
    {
        private TextElement(Font font, Models.Text.TextElement bTextElement, Color color)
        {
            ITextFont = font;
            ITextFont.Color = color;
            ITextFont.Size = bTextElement.FontSize.Points;
            Text = bTextElement.Text;
        }

        public Font ITextFont { get; set; }

        public PdfDocument PdfPdfDocument { get; set; }

        public Color Color { get; set; }

        public string Text { get; set; }

        public Models.Text.TextElement BusinessObject { get; set; }

        public static TextElement Parse(PdfDocument pdfDocument, Models.Text.TextElement bTextElement)
        {
            var font =
                new Font(pdfDocument.Fonts[bTextElement.FontStyle.Font.Key + "_" + bTextElement.FontStyle.Key].Font);

            font.Size = bTextElement.FontSize.Points;
            Color color = pdfDocument.Colors[bTextElement.Color.Key].RGBColor;
            return new TextElement(font, bTextElement, color);
        }

        public void Render(Paragraph itextParagraph)
        {
            var textChunk = new Chunk(Text, ITextFont);
            itextParagraph.Add(textChunk);
        }
    }
}