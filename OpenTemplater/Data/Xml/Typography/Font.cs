using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Typography
{
    public class Font
    {
        public string Key;
        public string BasePath;
        public string IsEmbedded;
        public string DefaultFontSize;
        public string Encoding;

        public List<FontStyle> Styles = new List<FontStyle>();

        public Font(System.Xml.XmlNode fontNode)
        {
            if (fontNode.Attributes["embed"] != null)
            {
                IsEmbedded = fontNode.Attributes["embed"].Value;
            }
            Key = fontNode.Attributes["key"].Value;
            BasePath = fontNode.Attributes["basepath"].Value;
            DefaultFontSize = fontNode.Attributes["defaultfontsize"].Value;
            Encoding = fontNode.Attributes["encoding"].Value;

            foreach (System.Xml.XmlNode fontStyleNode in fontNode.SelectNodes("style"))
            {
                FontStyle dFontStyle = new FontStyle(fontStyleNode);
                Styles.Add(dFontStyle);
            }
        }
    }
}