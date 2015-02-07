using System;
using OpenTemplater.Common.Exceptions;

namespace OpenTemplater.Data.Xml.Elements
{
    public class Rectangle : BaseElement, IXmlElement
    {
        public string Bordercolor;
        public string Borderwidth;
        public string Fillcolor;
        public string Roundness;
        public XmlContent XmlContent;

        public Rectangle(System.Xml.XmlNode rectangleNode)
        {
            this.Initialize(rectangleNode);

            try
            {
                Bordercolor = rectangleNode.Attributes["bordercolor"].Value;
                Borderwidth = rectangleNode.Attributes["borderwidth"].Value;

                if (rectangleNode.SelectSingleNode("content") != null)
                {
                    XmlContent = new XmlContent(rectangleNode.SelectSingleNode("content"));
                }
                if (rectangleNode.Attributes["fillcolor"] != null)
                {
                    Fillcolor = rectangleNode.Attributes["fillcolor"].Value;
                }
                if (rectangleNode.Attributes["roundness"] != null)
                {
                    Roundness = rectangleNode.Attributes["roundness"].Value;
                }
            }
            catch (NullReferenceException nrf)
            {
                throw new MissingAttributeException("", "rectangle");
            }
        }
    }
}