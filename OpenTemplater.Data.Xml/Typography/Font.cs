using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace OpenTemplater.Data.Xml.Typography
{
    public class Font
    {
        public string BasePath { get; private set; }
        public string DefaultFontSize { get; private set; }
        public string Encoding { get; private set; }
        public string IsEmbedded { get; private set; }
        public string Key { get; private set; }

        public List<FontStyle> Styles = new List<FontStyle>();

        public Font(XmlNode fontNode, bool useSystemFontFolder)
        {
            if (fontNode.Attributes != null)
            {
                if (fontNode.Attributes["embed"] != null)
                {
                    IsEmbedded = fontNode.Attributes["embed"].Value;
                }
                Key = fontNode.Attributes["key"].Value;

                BasePath = useSystemFontFolder
                               ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Fonts")
                               : fontNode.Attributes["basepath"].Value;
                DefaultFontSize = fontNode.Attributes["defaultfontsize"].Value;
                Encoding = fontNode.Attributes["encoding"].Value;
            }

            foreach (XmlNode fontStyleNode in fontNode.SelectNodes("style"))
            {
                var dFontStyle = new FontStyle(fontStyleNode);
                Styles.Add(dFontStyle);
            }
        }
    }
}