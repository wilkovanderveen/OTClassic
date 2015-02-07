using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml
{
    public class XmlContent
    {
        public List<Elements.IXmlElement> Elements
        {
            get;
            set;
        }

        public XmlContent(System.Xml.XmlNode contentNode)
        {
            Elements = new List<Elements.IXmlElement>();
           
            System.Xml.XmlNodeList elements = contentNode.SelectNodes("*");
            foreach (System.Xml.XmlNode element in elements)
            {
                switch (element.Name)
                {
                    case "text":
                        Elements.Add(new Elements.Text(element));
                        break;
                    case "image":
                        Elements.Add(new Elements.Image(element));
                        break;
                    case "rectangle":
                        Elements.Add(new Elements.Rectangle(element));
                        break;
                    case "line":
                        Elements.Add(new Elements.Line(element));
                        break;
                }
            }
        }
    }
}