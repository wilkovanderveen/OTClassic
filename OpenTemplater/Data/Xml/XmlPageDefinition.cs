using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Data.Xml.Layout;

namespace OpenTemplater.Data.Xml
{
    public class XmlPageDefinition : IXmlPageDefinition
    {
        public XmlContent XmlContent { get; private set; }

        public XmlTemplateLayout Layout { get; private set; }

        public string Width { get; private set; }
        public string Height { get; private set; }
        public string Colortype { get; private set; }
        public string Bleeding { get; private set; }
        public string Key { get; private set; }

        public XmlPageDefinition(System.Xml.XmlNode page)
        {
            Key = page.Attributes["key"].Value;
            Height = page.Attributes["height"].Value;
            Width = page.Attributes["width"].Value;
            Colortype = page.Attributes["colortype"].Value;
            if (page.Attributes["bleeding"] != null)
            {
                Bleeding = page.Attributes["bleeding"].Value;
            }

            XmlContent = new XmlContent(page.SelectSingleNode("content"));
            Layout = new XmlTemplateLayout(page.SelectSingleNode("layout"));
        }
    }
}