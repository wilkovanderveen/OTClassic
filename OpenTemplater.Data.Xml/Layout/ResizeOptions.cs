using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenTemplater.Data.Xml.Layout
{
    public class ResizeOptions
    {
        public string Type { get; set; }
        public string Maximum { get; set; }
        public string Minimum { get; set; }

        public ResizeOptions(System.Xml.XmlNode resizingNode)
        {
            Type = resizingNode.Attributes["type"].Value;

            XmlAttribute maxAttribute = resizingNode.Attributes["maximum"];
            Maximum = maxAttribute != null ? maxAttribute.Value : null;

            XmlAttribute minAttribute = resizingNode.Attributes["minimum"];
            Minimum = minAttribute != null ? minAttribute.Value : null;
        }
    }
}