using System.Xml;

namespace OpenTemplater.Data.Xml.Typography
{
    public class Color
    {
        public CMYKColor CMYKColor;
        public string Key;
        public PMSColor PMSColor;
        public RGBColor RGBColor;

        public Color(XmlNode colorNode)
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