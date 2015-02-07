using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Elements
{
    public class Text : BaseElement, IXmlElement
    {
        #region public properties

        public string Name
        {
            get { return "text"; }
        }

        #endregion

        #region public members

        public string VerticalAligment;
        public List<Paragraph> Paragraphs = new List<Paragraph>();
        public string OverflowElement;

        #endregion

        public Text(System.Xml.XmlNode textNode)
        {
            this.Initialize(textNode);
            foreach (System.Xml.XmlNode paragraphNode in textNode.SelectNodes("paragraphs/paragraph"))
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

            #endregion
        }
    }
}