using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenTemplater.Data.Xml.Layout;

namespace OpenTemplater.Data.Xml
{
    public class XmlPageTemplateDefinition : IXmlPageDefinition
    {
        public XmlContent XmlDynamicContent { get; private set; }
        public XmlContent XmlStaticContent { get; private set; }

        public string Width { get; private set; }
        public string Height { get; private set; }
        public string Colortype { get; private set; }
        public string Bleeding { get; private set; }
        public string Key { get; private set; }
        public XmlLayoutDefinition XmlLayout { get; private set; }
        

        public XmlPageTemplateDefinition(System.Xml.XmlNode template)
        {
            Key = template.Attributes["key"].Value;
            Height = template.Attributes["height"].Value;
            Width = template.Attributes["width"].Value;
            Colortype = template.Attributes["colortype"].Value;
            if (template.Attributes["bleeding"] != null)
            {
                Bleeding = template.Attributes["bleeding"].Value;
            }

            XmlNode dynamicXmlContentNode = template.SelectSingleNode("dynamicContent");
            XmlNode staticContentNode = template.SelectSingleNode("staticContent");

            if (staticContentNode != null)
            {
                XmlStaticContent = new XmlContent(staticContentNode);
            }

            if (dynamicXmlContentNode != null)
            {
                XmlDynamicContent = new XmlContent(dynamicXmlContentNode);
            }
            
            XmlLayout = new XmlLayoutDefinition(template.SelectSingleNode("layout"));
        }
    }
}