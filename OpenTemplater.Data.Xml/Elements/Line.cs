using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Elements
{
    /// <summary>
    /// XML-object representation for a line element.
    /// </summary>
    public class Line : BaseElement, IXmlElement
    {
        public string Color;
        public string Width;

        /// <summary>
        /// Constructs a xml-object from an xml-node.
        /// </summary>
        /// <param name="lineNode">XmlNode which contains the line definition.</param>
        public Line(System.Xml.XmlNode lineNode)
        {
            this.Initialize(lineNode);

            try
            {
                Color = lineNode.Attributes["color"].Value;
                Width = lineNode.Attributes["width"].Value;
            }
            catch (NullReferenceException nex)
            {
                throw new RequiredAttributeNotFoundException("", "line", this.Key);
            }
           
        }
    }
}