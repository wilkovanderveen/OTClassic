using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Typography
{
    public class PMSColor
    {
        public string Name;

        public PMSColor(System.Xml.XmlNode pmsColorNode)
        {
            Name = pmsColorNode.Attributes["name"].Value;
        }
    }
}