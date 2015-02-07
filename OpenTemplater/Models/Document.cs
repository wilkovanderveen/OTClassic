using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;
using NLog;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Core.Modules;
using OpenTemplater.Data.Xml;
using OpenTemplater.Data.Xml.Elements;
using OpenTemplater.Data.Xml.Typography;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Layout;
using OpenTemplater.Models.Typography.Collections;
using CMYKColor = OpenTemplater.Models.Typography.CMYKColor;
using PMSColor = OpenTemplater.Models.Typography.PMSColor;
using RGBColor = OpenTemplater.Models.Typography.RGBColor;

namespace OpenTemplater.Models
{
    public class Document
    {
        #region private members

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ColorCollection _colors =
            new ColorCollection();

        private readonly ConfigSettings _configuration;

        private readonly FontCollection _fonts =
            new FontCollection();

        private readonly PageCollection _pages = new PageCollection();
        private readonly PageCollection _staticPages = new PageCollection();

        private readonly PageTemplateCollection _templates =
            new PageTemplateCollection();

        private string _author;
        private int _dpi = 360;
        private Uri _fileUri;
        private string _subject;
        private string _title;

        #endregion

        #region public properties

        public PageCollection Pages
        {
            get { return _pages; }
        }

        public FontCollection Fonts
        {
            get { return _fonts; }
        }

        public ColorCollection Colors
        {
            get { return _colors; }
        }

        /// <summary>
        /// Author of the current document.
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public Uri Uri
        {
            get { return _fileUri; }
        }


        public int Dpi
        {
            get { return _dpi; }
        }

        #endregion

        public Document()
        {
            _configuration = new ConfigSettings();
        }

        private Document(string fileUri) : this()
        {
            _fileUri = new Uri(fileUri);
        }

        public void Load(XmlDocumentDefinition xmlDataDocument)
        {
            // If the fontpath is set, update template configuration.
            if (!string.IsNullOrEmpty(xmlDataDocument.FontPath))
            {
                _configuration.SetFontPath(xmlDataDocument.FontPath, xmlDataDocument.FontPathRelative);
            }

            if (!string.IsNullOrEmpty(xmlDataDocument.Dpi))
            {
                _dpi = Convert.ToInt32(xmlDataDocument.Dpi);
            }

            // Load metadata
            if (xmlDataDocument.Author != null)
            {
                _author = xmlDataDocument.Author;
            }
            if (xmlDataDocument.Subject != null)
            {
                _subject = xmlDataDocument.Subject;
            }
            if (xmlDataDocument.Title != null)
            {
                _title = xmlDataDocument.Title;
            }


            // Load fonts
            foreach (Font dFont in xmlDataDocument.Fonts)
            {
                var bFont = new Typography.Font(dFont.Key, _configuration.FontPath + dFont.BasePath, dFont.Encoding,
                                                dFont.IsEmbedded,
                                                dFont.DefaultFontSize);
                foreach (FontStyle dFontStyle in dFont.Styles)
                {
                    bFont.AddStyle(new Typography.FontStyle(bFont, dFontStyle.Key, dFontStyle.File));
                }
                Fonts.Add(bFont);
            }

            // Load colors
            foreach (Color color in xmlDataDocument.Colors)
            {
                var bColor = new Typography.Color(color.Key);
                bColor.CMYKColor = new CMYKColor(color.CMYKColor.Cyan,
                                                 color.CMYKColor.Magenta,
                                                 color.CMYKColor.Yellow,
                                                 color.CMYKColor.Black,
                                                 color.CMYKColor.Tint);
                bColor.RGBColor = new RGBColor(color.RGBColor.Red,
                                               color.RGBColor.Green,
                                               color.RGBColor.Blue);

                if (color.PMSColor != null)
                {
                    bColor.PMSColor = new PMSColor(color.PMSColor.Name);
                }
                Colors.Add(bColor);
            }

            // Load pages
            int pageNumber = 1;

            foreach (XmlPageDefinition dataPage in xmlDataDocument.Pages)
            {
                var bPage = new Page(this, dataPage.Key, dataPage.Width, dataPage.Height, dataPage.Colortype,
                                     dataPage.Bleeding, pageNumber);
                _staticPages.Add(bPage);

                // Add Elements
                ProcessContent(dataPage.XmlContent, bPage.Contents);
            }

            // Load pages template.
            foreach (XmlPageTemplateDefinition dataTemplate in xmlDataDocument.PageTemplates)
            {
                var pageTemplate = new PageTemplate(this, dataTemplate.Key, dataTemplate.Width,
                                                    dataTemplate.Height, dataTemplate.Colortype,
                                                    dataTemplate.Bleeding, dataTemplate.XmlLayout.VOffset.Value,
                                                    dataTemplate.XmlLayout.HOffset.Value,
                                                    dataTemplate.XmlLayout.Width.Value,
                                                    dataTemplate.XmlLayout.Height.Value);
                _templates.Add(pageTemplate);

                // Add dynamic elements (if any)
                ProcessContent(dataTemplate.XmlDynamicContent, pageTemplate.DynamicContents);

                // Add static elements (if any)
                ProcessContent(dataTemplate.XmlStaticContent, pageTemplate.StaticContents);
            }

            PageSequence pageSequence = PageSequence.Parse(this, xmlDataDocument);

            // Create page Sequence.
            var pagingModule = new PagingModule();
            _pages.AddRange(pagingModule.GetPages(pageSequence, _staticPages, _templates));
        }

        private void ProcessContent(XmlContent xmlContent, Content businessContent)
        {
            if (xmlContent != null)
            {
                foreach (IXmlElement dataElement in xmlContent.Elements)
                {
                    switch (dataElement.GetType().Name.ToLower())
                    {
                        case "text":
                            var dataText = (Data.Xml.Elements.Text) dataElement;
                            var bText = new Text.Text(businessContent, dataElement.Key,
                                                      string.IsNullOrEmpty(dataText.Rotation)
                                                          ? 0
                                                          : float.Parse(dataText.Rotation));
                            bText.LayoutContainer = new LayoutContainer(bText, dataElement.XmlLayoutDefinition);
                            foreach (Paragraph dParagraph in dataText.Paragraphs)
                            {
                                var bParagraph = new Text.Paragraph(bText,
                                                                    dParagraph.Leading,
                                                                    dParagraph.Alignment);

                                bParagraph.Offset = dParagraph.Offset != null ? new Unit(dParagraph.Offset) : null;
                                bParagraph.Symbol = dParagraph.Symbol != null ? dParagraph.Symbol : null;

                                bText.Add(bParagraph);

                                foreach (TextElement dTextElement in dParagraph.TextElements)
                                {
                                    var bTextElement = new Text.TextElement(bParagraph,
                                                                            dTextElement
                                                                                .Value,
                                                                            dTextElement
                                                                                .Font,
                                                                            dTextElement
                                                                                .Style,
                                                                            dTextElement
                                                                                .
                                                                                Fontsize,
                                                                            dTextElement
                                                                                .Color);
                                    if (!string.IsNullOrEmpty(dTextElement.Charspacing))
                                    {
                                        bTextElement.CharSpacing = new Unit(dTextElement.Charspacing);
                                    }
                                    bParagraph.Add(bTextElement);
                                }
                            }
                            if (!string.IsNullOrEmpty(dataElement.ZOrder))
                            {
                                bText.ZOrder = Convert.ToUInt16(dataElement.ZOrder);
                            }

                            if (!string.IsNullOrEmpty(dataText.OverflowElement))
                            {
                                bText.SetOverflowElement(dataText.OverflowElement);
                            }

                            bText.SetTextlines();

                            businessContent.Elements.Add(bText);
                            break;

                        case "line":
                            var dataLine = (Data.Xml.Elements.Line) dataElement;
                            var bLine = new Line(businessContent, dataElement.Key, dataLine.Width, dataLine.Color);
                            bLine.LayoutContainer = new LayoutContainer(bLine, dataElement.XmlLayoutDefinition);

                            if (!string.IsNullOrEmpty(dataElement.ZOrder))
                            {
                                bLine.ZOrder = Convert.ToUInt16(dataElement.ZOrder);
                            }
                            businessContent.Elements.Add(bLine);

                            break;

                        case "image":
                            var dataImage = (Data.Xml.Elements.Image) dataElement;
                            var bImage = new Image(businessContent, dataImage.Key, dataImage.Path);
                            bImage.LayoutContainer = new LayoutContainer(bImage, dataElement.XmlLayoutDefinition);

                            bImage.ConvertDPI();

                            if (!string.IsNullOrEmpty(dataElement.ZOrder))
                            {
                                bImage.ZOrder = Convert.ToUInt16(dataElement.ZOrder);
                            }

                            businessContent.Elements.Add(bImage);

                            break;

                        case "rectangle":
                            var dataRectangle = (Data.Xml.Elements.Rectangle) dataElement;

                            var bRectangle = new Rectangle(businessContent, dataRectangle.Key,
                                                           dataRectangle.Bordercolor, dataRectangle.Borderwidth);
                            bRectangle.LayoutContainer = new LayoutContainer(bRectangle, dataElement.XmlLayoutDefinition);
                            bRectangle.Contents = new Content(bRectangle);

                            if (dataRectangle.XmlContent != null)
                            {
                                ProcessContent(dataRectangle.XmlContent, bRectangle.Contents);
                                bRectangle.Contents.UpdateContentHeight();
                                bRectangle.LayoutContainer.TryResize(bRectangle.Layout.Width, bRectangle.Contents.Height);
                                bRectangle.Relocate();
                            }
                            if (!string.IsNullOrEmpty(dataRectangle.Fillcolor))
                            {
                                bRectangle.FillColor = businessContent.Page.Document.Colors[dataRectangle.Fillcolor];
                            }
                            if (!string.IsNullOrEmpty(dataRectangle.Roundness))
                            {
                                bRectangle.Roundness = new Unit(dataRectangle.Roundness);
                            }
                            if (!string.IsNullOrEmpty(dataElement.ZOrder))
                            {
                                bRectangle.ZOrder = Convert.ToUInt16(dataElement.ZOrder);
                            }


                            businessContent.Elements.Add(bRectangle);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Loads a document based on a file.
        /// </summary>
        /// <param name="uri"></param>
        public void Load(string uri)
        {
            _fileUri = new Uri(uri);
            var dataDocument = new XmlDocumentDefinition(uri);
            Load(dataDocument);
        }

        /// <summary>
        /// Constructs an document object based on a XML input file and a XSL stylesheet.
        /// </summary>
        /// <param name="xmlUri">URI to the XML file.</param>
        /// <param name="xslUri">URI to the XSL stylesheet.</param>
        public void Load(string xmlUri, string xslUri)
        {
            var xslTransform = new XslTransform();
            xslTransform.Load(xslUri);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlUri);

            var resultStream = new MemoryStream();


            xslTransform.Transform(xmlDocument, new XsltArgumentList(), resultStream);
            resultStream.Position = 0;

            var dataDocument = new XmlDocumentDefinition(resultStream);
            Load(dataDocument);
        }

        public void XsdValidationHandler(object sender, ValidationEventArgs args)
        {
            logger.ErrorException(args.Message, args.Exception);
        }

        public void Load(string xmlUri, string xslUri, string xsdUri)
        {
            var xslTransform = new XslTransform();
            xslTransform.Load(xslUri);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlUri);

            var resultStream = new MemoryStream();


            xslTransform.Transform(xmlDocument, new XsltArgumentList(), resultStream);
            resultStream.Position = 0;

            // check via XSD.
            try
            {
                Console.WriteLine("Validating XML");

                var settings = new XmlReaderSettings();

                settings.NameTable = new NameTable();
                var nsManager = new XmlNamespaceManager(settings.NameTable);
                nsManager.AddNamespace("", "http://stroop.mine.nu/xsd/OpenTemplater");
                var xmlParserContext = new XmlParserContext(settings.NameTable, nsManager, "", XmlSpace.Default);

                settings.Schemas.Add(null, xsdUri);
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += XsdValidationHandler;

                var document = new XmlDocument();
                document.Load(resultStream);

                XmlReader rdr = XmlReader.Create(new StringReader(document.InnerXml), settings, xmlParserContext);
                while (rdr.Read())
                {
                }

                document.Save("F:\\debug.xml");


                Console.WriteLine("Succesfully validated XML.");

                var dataDocument = new XmlDocumentDefinition(document);
                Load(dataDocument);
            }
            catch (XmlSchemaValidationException ex)
            {
            }
        }

        public static Document Create(string filepath)
        {
            return new Document(filepath);
        }

        public XmlDocument ToXml()
        {
            var documentBase = new XmlDocument();
            documentBase.Load("./Resources/Xml/TemplateBase.xsl");
            var xnsm = new XmlNamespaceManager(documentBase.NameTable);
            xnsm.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
            xnsm.AddNamespace("", "http://stroop.mine.nu/xsd");

            documentBase.SelectSingleNode("/xsl:stylesheet/xsl:template[@match='document']/template/metadata", xnsm).
                InnerXml = CreateMetaDataXml();

            return documentBase;
        }

        private string CreateMetaDataXml()
        {
            var metaDataString = new StringBuilder();

            metaDataString.AppendFormat("<title>{0}</title>", _title);
            metaDataString.AppendFormat("<author>{0}</author>", _author);
            metaDataString.AppendFormat("<subject>{0}</subject>", _subject);

            return metaDataString.ToString();
        }
    }
}