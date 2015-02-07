using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OpenTemplater.Output.PDF.Typography;
using Color = OpenTemplater.Output.PDF.Typography.Color;

namespace OpenTemplater.Output.PDF
{
    public class PdfDocument
    {
        private iTextSharp.text.Document _itextDocument;
        private iTextSharp.text.pdf.PdfWriter _itextPDFWriter;
        private ColorList _colors = new ColorList();
        private FontStyleList _fontStyles = new FontStyleList();

        internal PdfWriter ITextPdfWriter
        {
            get { return _itextPDFWriter; }
        }

        public PdfDocument(Models.Document documentTemplate, string outputFile)
        {
            // Create Colors
            foreach (Models.Typography.Color bColor in documentTemplate.Colors)
            {
                Color itextColor = new Color(bColor);
                Colors.Add(itextColor);
            }

            // Create Fonts
            foreach (Models.Typography.Font bFont in documentTemplate.Fonts)
            {
                foreach (Models.Typography.FontStyle bFontStyle in bFont.Styles)
                {
                    FontStyle itextFontStyle =
                        new FontStyle(bFontStyle);
                    Fonts.Add(itextFontStyle);
                }
            }

            try
            {
                CreatePages(documentTemplate, outputFile);
                SetDocumentProperties(documentTemplate);

                _itextDocument.Close();
                _itextPDFWriter.Close();
            }
            catch (IOException iox)
            {
                throw;
            }
        }

        public void SetDocumentProperties(Models.Document documentTemplate)
        {
            if (documentTemplate.Author != null)
            {
                _itextDocument.AddAuthor(documentTemplate.Author);
            }
            if (documentTemplate.Title != null)
            {
                _itextDocument.AddTitle(documentTemplate.Title);
            }
            if (documentTemplate.Subject != null)
            {
                _itextDocument.AddSubject(documentTemplate.Subject);
            }
        }

        public void CreatePages(Models.Document documentTemplate, string outputFile)
        {
            _itextDocument = new iTextSharp.text.Document();
            _itextPDFWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(_itextDocument,
                                                                            new System.IO.FileStream(outputFile,
                                                                                                     System.IO.
                                                                                                         FileMode.
                                                                                                         Create));

            _itextPDFWriter.PdfVersion = PdfWriter.VERSION_1_7;
            _itextPDFWriter.Open();

            PdfContentByte itextContent = _itextPDFWriter.DirectContent;

            int pageCounter = 0;

            foreach (Models.Page pageTemplate in documentTemplate.Pages)
            {
                pageCounter++;

                if (pageTemplate.Bleeding.Points > 0)
                {
                    _itextPDFWriter.SetBoxSize("bleed",
                                                      new iTextSharp.text.Rectangle(0, 0, pageTemplate.Width.Points,
                                                                                    pageTemplate.Height.Points));
                }

                Rectangle pageLayoutRectangle = new iTextSharp.text.Rectangle(-pageTemplate.Bleeding.Points,
                                                                              -pageTemplate.Bleeding.Points,
                                                                              pageTemplate.Width.Points +
                                                                              pageTemplate.Bleeding.Points,
                                                                              pageTemplate.Height.Points +
                                                                              pageTemplate.Bleeding.Points);

                _itextPDFWriter.PageEmpty = false;
                _itextDocument.SetPageSize(pageLayoutRectangle);

                if (!_itextDocument.IsOpen())
                    _itextDocument.Open();



                PdfPage page = new PdfPage(this, itextContent);
                page.Render(pageTemplate);
                itextContent.PdfDocument.NewPage();
            }
        }




        public ColorList Colors
        {
            get { return _colors; }
            set { _colors = value; }
        }

        public FontStyleList Fonts
        {
            get { return _fontStyles; }
            set { _fontStyles = value; }
        }

        private Chunk CreateChunk(Models.Text.TextElement textElement)
        {
            Chunk text = new Chunk(textElement.Text);
            text.Font = Fonts[textElement.FontStyle.CollectionKey].Font;
            text.Font.Color = Colors[textElement.Color.Key].CMYKColor;
            text.Font.Size = textElement.FontSize.Points;

            return text;
        }

        private Phrase CreateTextPhrase(Models.Text.TextElement textElement, out float elementWidth)
        {
            Chunk pTextChunk = new Chunk(textElement.Text, Fonts[textElement.FontStyle.CollectionKey].Font);

            pTextChunk.Font.Color = Colors[textElement.Color.Key].CMYKColor;
            pTextChunk.Font.Size = textElement.FontSize.Points;


            Phrase pTextPhrase = new Phrase(pTextChunk);

            elementWidth = pTextChunk.GetWidthPoint();

            return pTextPhrase;
        }
    }
}