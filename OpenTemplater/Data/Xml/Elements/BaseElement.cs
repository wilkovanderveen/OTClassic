using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Elements
{
    public class BaseElement
    {
        private string _key;
        private Layout.XmlLayoutDefinition _xmlLayoutDefinition;
        private string _zOrder;

        public string Key
        {
            get { return _key; }
        }

        public Layout.XmlLayoutDefinition XmlLayoutDefinition
        {
            get { return _xmlLayoutDefinition; }
        }

        public string ZOrder
        {
            get { return _zOrder; }
        }

        public void Initialize(System.Xml.XmlNode elementNode)
        {
            _key = elementNode.Attributes["key"].Value;
            if (elementNode.Attributes["zorder"] != null)
            {
                _zOrder = elementNode.Attributes["zorder"].Value;
            }

            _xmlLayoutDefinition = new Layout.XmlLayoutDefinition(elementNode.SelectSingleNode("layout"));
        }
    }
}