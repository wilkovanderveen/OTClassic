using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Elements
{
    public interface IXmlElement
    {
        string Key { get; }
        string ZOrder { get; }
        Layout.XmlLayoutDefinition XmlLayoutDefinition { get; }
        void Initialize(System.Xml.XmlNode elementNode);
    }
}