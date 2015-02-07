using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Typography
{
    public class Color
    {
        public string Key;
        public CMYKColor CMYKColor;
        public RGBColor RGBColor;
        public PMSColor PMSColor;

        public Color(System.Xml.XmlNode colorNode)
        {
            Key = colorNode.Attributes["key"].Value;

            CMYKColor = new CMYKColor(colorNode.SelectSingleNode("cmyk"));
            RGBColor = new RGBColor(colorNode.SelectSingleNode("rgb"));
            if (colorNode.SelectSingleNode("pms") != null)
            {
                PMSColor = new PMSColor(colorNode.SelectSingleNode("pms"));
            }
        }
    }
}