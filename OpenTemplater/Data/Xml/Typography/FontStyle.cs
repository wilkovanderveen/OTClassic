using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Typography
{
    public class FontStyle
    {
        public string Key;
        public string File;

        public FontStyle(System.Xml.XmlNode fontStyleNode)
        {
            Key = fontStyleNode.Attributes["key"].Value;
            File = fontStyleNode.Attributes["file"].Value;
        }
    }
}