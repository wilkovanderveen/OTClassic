using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Elements
{
    public class Image : BaseElement, IXmlElement
    {
        public string Path;

        public Image(System.Xml.XmlNode imageNode)
        {
            this.Initialize(imageNode);

            Path = imageNode.Attributes["path"].Value;
        }
    }
}