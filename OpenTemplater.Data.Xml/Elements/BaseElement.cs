using System;
using System.Xml;
using OpenTemplater.Data.Xml.Layout;

namespace OpenTemplater.Data.Xml.Elements
{
    public class BaseElement
    {
        private string _key;
        private XmlLayoutDefinition _xmlLayoutDefinition;
        private string _zOrder;

        public string Key
        {
            get { return _key; }
        }

        public XmlLayoutDefinition XmlLayoutDefinition
        {
            get { return _xmlLayoutDefinition; }
        }

        public string ZOrder
        {
            get { return _zOrder; }
        }

        public void Initialize(XmlNode elementNode)
        {
            {
                _key = elementNode.Attributes["key"].Value;
                if (elementNode.Attributes["zorder"] != null)
                {
                    _zOrder = elementNode.Attributes["zorder"].Value;
                }

                _xmlLayoutDefinition = new XmlLayoutDefinition(elementNode.SelectSingleNode("layout"));
            }
           
        }
    }
}