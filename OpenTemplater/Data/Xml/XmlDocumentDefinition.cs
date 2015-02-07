using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenTemplater.Data.Xml.Typography;

namespace OpenTemplater.Data.Xml
{
    public class XmlDocumentDefinition
    {
        public List<XmlPageDefinition> Pages = new List<XmlPageDefinition>();
        public List<XmlPageSequenceElement> PageSequenceElements = new List<XmlPageSequenceElement>();
        public List<XmlPageTemplateDefinition> PageTemplates = new List<XmlPageTemplateDefinition>();
        public List<Font> Fonts = new List<Font>();
        public List<Color> Colors = new List<Color>();

        public string Author;
        public string Title;
        public string Subject;

        public XmlDocumentDefinition(System.IO.MemoryStream stream)
        {
            System.Xml.XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);
            Init(xmlDocument);
        }

        public XmlDocumentDefinition(string uri)
        {
            System.Xml.XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(uri);
            Init(xmlDocument);
        }

        public XmlDocumentDefinition(XmlDocument document)
        {
            Init(document);
        }

        private void Init(XmlDocument xmlDocument)
        {
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
            foreach (System.Xml.XmlNode color in colors)
            {
                Colors.Add(new Color(color));
            }

            XmlNodeList fonts = xmlDocument.SelectNodes("/template/fonts/font");
            foreach (System.Xml.XmlNode font in fonts)
            {
                Fonts.Add(new Font(font));
            }

            XmlNodeList pageSequence = xmlDocument.SelectNodes("/template/document/pageSequence/page");
            foreach (System.Xml.XmlNode pageSequenceNode in pageSequence)
            {
                PageSequenceElements.Add(new XmlPageSequenceElement(pageSequenceNode));
            }

            XmlNodeList pages = xmlDocument.SelectNodes("/template/document/page");
            foreach (System.Xml.XmlNode page in pages)
            {
                Pages.Add(new XmlPageDefinition(page));
            }

            XmlNodeList pagetemplates = xmlDocument.SelectNodes("/template/document/pageTemplate");
            foreach (System.Xml.XmlNode pagetemplate in pagetemplates)
            {
                PageTemplates.Add(new XmlPageTemplateDefinition(pagetemplate));
            }
        }
    }
}