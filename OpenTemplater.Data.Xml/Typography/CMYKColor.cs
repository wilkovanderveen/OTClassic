using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Typography
{
    public class CMYKColor
    {
        public string Cyan;
        public string Magenta;
        public string Yellow;
        public string Black;
        public string Tint;

        public CMYKColor(System.Xml.XmlNode cmykColorNode)
        {
            Cyan = cmykColorNode.Attributes["cyan"].Value;
            Magenta = cmykColorNode.Attributes["magenta"].Value;
            Yellow = cmykColorNode.Attributes["yellow"].Value;
            Black = cmykColorNode.Attributes["black"].Value;

            Tint = cmykColorNode.Attributes["tint"] != null ? cmykColorNode.Attributes["tint"].Value : null;
        }
    }
}