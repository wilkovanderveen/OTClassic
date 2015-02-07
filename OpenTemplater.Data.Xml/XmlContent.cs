using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Data.Xml.Elements;

namespace OpenTemplater.Data.Xml
{
    public class XmlContent
    {
        public List<IXmlElement> Elements
        {
            get; private set;
        }

        public XmlContent(System.Xml.XmlNode contentNode)
        {
            Elements = new List<IXmlElement>();
           
            System.Xml.XmlNodeList elements = contentNode.SelectNodes("*");
           
            foreach (System.Xml.XmlNode element in elements)
            {
                switch (element.Name)
                {
                    case "text":
                        Elements.Add(new Text(element));
                        break;
                    case "image":
                        Elements.Add(new Image(element));
                        break;
                    case "rectangle":
                        Elements.Add(new Rectangle(element));
                        break;
                    case "line":
                        Elements.Add(new Line(element));
                        break;
                    default:
                        throw new NotSupportedException(string.Format("Element of type {0} is not supported.",
                                                                      element.Name));
                }
            }
        }
    }
}