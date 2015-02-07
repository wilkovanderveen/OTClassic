using System.Collections.Generic;
using System.Xml;

namespace OpenTemplater.Data.Xml.Elements
{
    public class Text : BaseElement, IXmlElement
    {
        public List<Paragraph> Paragraphs = new List<Paragraph>();

        public Text(XmlNode textNode)
        {
            Initialize(textNode);
            foreach (XmlNode paragraphNode in textNode.SelectNodes("paragraphs/paragraph"))
            {
                Paragraphs.Add(new Paragraph(paragraphNode));
            }

            #region optional attributes.

            if (textNode.Attributes["verticalalign"] != null)
            {
                VerticalAligment = textNode.Attributes["verticalalign"].Value;
            }
            if (textNode.Attributes["overflowelement"] != null)
            {
                OverflowElement = textNode.Attributes["overflowelement"].Value;
            }
            if (textNode.Attributes["rotation"] != null)
            {
                Rotation = textNode.Attributes["rotation"].Value;
            }

            #endregion
        }

        public string Name
        {
            get { return "text"; }
        }

        public string OverflowElement { get; private set; }
        public string VerticalAligment { get; private set; }
        public string Rotation { get; private set; }
    }
}