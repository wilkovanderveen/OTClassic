using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Typography
{
    public class RGBColor
    {
        public string Red;
        public string Green;
        public string Blue;
        public string Alpha;

        public RGBColor(System.Xml.XmlNode rgbColorNode)
        {
            Red = rgbColorNode.Attributes["red"].Value;
            Green = rgbColorNode.Attributes["green"].Value;
            Blue = rgbColorNode.Attributes["blue"].Value;

            Alpha = rgbColorNode.Attributes["alpha"] != null ? rgbColorNode.Attributes["tint"].Value : null;
        }
    }
}