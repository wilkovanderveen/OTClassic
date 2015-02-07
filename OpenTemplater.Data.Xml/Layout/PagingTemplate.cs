using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenTemplater.Data.Xml.Layout
{
    public class PagingTemplate
    {
        public string ReferenceKey;
        public string TemplateKey;
        public OverflowSetting OnVerticalOverflow;

        public PagingTemplate(XmlNode settingsNode)
        {
            XmlAttribute pageReferenceAttribute = settingsNode.Attributes["staticref"];
            ReferenceKey = pageReferenceAttribute != null ? pageReferenceAttribute.Value : null;

            XmlAttribute templateReferenceAttribute = settingsNode.Attributes["template"];
            TemplateKey = templateReferenceAttribute != null ? templateReferenceAttribute.Value : null;

            XmlNode onVerticalOverflow = settingsNode.SelectSingleNode("onVerticalOverflow");
        }
    }
}
