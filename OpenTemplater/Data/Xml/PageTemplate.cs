using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml
{
    public class PageTemplate : BasePage
    {
        public Sequence Sequence;

        public PageTemplate(System.Xml.XmlNode pageTemplateNode)
        {
            Key = pageTemplateNode.Attributes["key"].Value;
            Height = pageTemplateNode.Attributes["height"].Value;
            Width = pageTemplateNode.Attributes["width"].Value;
            Colortype = pageTemplateNode.Attributes["colortype"].Value;
            if (pageTemplateNode.Attributes["bleeding"] != null)
            {
                Bleeding = pageTemplateNode.Attributes["bleeding"].Value;
            }

            // Process content.
            Content = new Content(pageTemplateNode.SelectSingleNode("content"));

            
        }
    }
}
