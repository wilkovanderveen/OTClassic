using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenTemplater.Data.Xml.Elements
{
    public class Paragraph
    {
        public String Leading;
        public String Alignment;
        public String Offset;
        public String Symbol;
        public List<TextElement> TextElements = new List<TextElement>();

        public Paragraph(XmlNode paragraphNode)
        {
            Leading = paragraphNode.Attributes["leading"].Value;
            Alignment = paragraphNode.Attributes["alignment"].Value;

            XmlAttribute offsetAttribute = paragraphNode.Attributes["offset"];
            Offset = offsetAttribute != null ? offsetAttribute.Value : null;

            XmlAttribute symbolAttribute = paragraphNode.Attributes["symbol"];
            Symbol = symbolAttribute != null ? symbolAttribute.Value : null;

            foreach (XmlNode textElementNode in paragraphNode.SelectNodes("textelement"))
            {
                TextElements.Add(new TextElement(textElementNode));
            }
        }
    }
}