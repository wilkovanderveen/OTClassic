using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using OpenTemplater.Data.Xml.Typography;

namespace OpenTemplater.Data.Xml
{
    public class XmlDocumentDefinition
    {
        public string Author;
        public List<Color> Colors = new List<Color>();
        public string Dpi;

        public string FontPath;
        public bool FontPathRelative;
        public List<Font> Fonts = new List<Font>();
        public List<XmlPageSequenceElement> PageSequenceElements = new List<XmlPageSequenceElement>();
        public List<XmlPageTemplateDefinition> PageTemplates = new List<XmlPageTemplateDefinition>();
        public List<XmlPageDefinition> Pages = new List<XmlPageDefinition>();
        public string Subject;
        public string Title;

        public XmlDocumentDefinition(MemoryStream stream)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);
            Init(xmlDocument);
        }

        /// <summary>
        /// Creates a new document definition from an XML file.
        /// </summary>
        /// <param name="uri"></param>
        public XmlDocumentDefinition(string uri)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(uri);
            Init(xmlDocument);
        }

        public XmlDocumentDefinition(XmlDocument document)
        {
            Init(document);
        }

        private void Init(XmlDocument xmlDocument)
        {
            // Get configuration
            XmlNode configXmlNode = xmlDocument.SelectSingleNode("/template/configuration");

            if (configXmlNode != null)
            {
                XmlNode fontPathNode = configXmlNode.SelectSingleNode("fontPath");
                if (fontPathNode != null)
                {
                    FontPath = fontPathNode.InnerText;
                    XmlAttribute isRelativeAttribute = fontPathNode.Attributes["relative"];
                    if (isRelativeAttribute != null)
                    {
                        FontPathRelative = Convert.ToBoolean(isRelativeAttribute.Value);
                    }
                }
            }

            // Get metadata
            if (xmlDocument.SelectSingleNode("/template/metadata/subject") != null)
            {
                Subject = xmlDocument.SelectSingleNode("/template/metadata/subject").InnerText;
            }

            if (xmlDocument.SelectSingleNode("/template/metadata/author") != null)
            {
                Author = xmlDocument.SelectSingleNode("/template/metadata/author").InnerText;
            }

            if (xmlDocument.SelectSingleNode("/template/metadata/title") != null)
            {
                Title = xmlDocument.SelectSingleNode("/template/metadata/title").InnerText;
            }

            XmlNodeList colors = xmlDocument.SelectNodes("/template/colors/color");
            foreach (XmlNode color in colors)
            {
                Colors.Add(new Color(color));
            }

            XmlNode fontsCollectionNode = xmlDocument.SelectSingleNode("/template/fonts");

            bool useSystemFontsFolder = false;

            if (fontsCollectionNode != null)
            {
                if (fontsCollectionNode.Attributes != null)
                {
                    XmlAttribute useSystemFontFolderAttribute =
                        fontsCollectionNode.Attributes["useSystemFontFolder"];

                    if (useSystemFontFolderAttribute != null)
                    {
                        if (!bool.TryParse(useSystemFontFolderAttribute.Value, out useSystemFontsFolder))
                        {
                            throw new ArgumentException("useSystemFontFolder should be true or false.");
                        }
                    }
                }
                XmlNodeList fonts = xmlDocument.SelectNodes("/template/fonts/font");

                if (fonts != null)
                {
                    foreach (XmlNode font in fonts)
                    {
                        Fonts.Add(new Font(font, useSystemFontsFolder));
                    }
                }
            }

           

         
            
            if (xmlDocument.SelectSingleNode("/template/document").Attributes["dpi"] != null)
            {
                Dpi = xmlDocument.SelectSingleNode("/template/document").Attributes["dpi"].Value;
            }


            XmlNodeList pageSequence =
                xmlDocument.SelectNodes("/template/document/pageSequence/*[name()='staticPage' or name()='dynamicPage']");
            foreach (XmlNode pageSequenceNode in pageSequence)
            {
                PageSequenceElements.Add(new XmlPageSequenceElement(pageSequenceNode));
            }

            XmlNodeList pages = xmlDocument.SelectNodes("/template/document/page");
            foreach (XmlNode page in pages)
            {
                Pages.Add(new XmlPageDefinition(page));
            }

            XmlNodeList pagetemplates = xmlDocument.SelectNodes("/template/document/pageTemplate");
            foreach (XmlNode pagetemplate in pagetemplates)
            {
                PageTemplates.Add(new XmlPageTemplateDefinition(pagetemplate));
            }
        }
    }
}