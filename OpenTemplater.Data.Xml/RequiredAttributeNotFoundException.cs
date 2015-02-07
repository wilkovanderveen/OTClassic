using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml
{
    public class RequiredAttributeNotFoundException : Exception
    {
        private readonly string _attributeName;
        private readonly string _elementName;
        private readonly string _elementKey;

        public override string Message
        {
            get
            {
                return "Required attribute " + _attributeName + " not found for element of type " + _elementName + " with key " + _elementKey + "."; 
            }
        }

        public RequiredAttributeNotFoundException(string attribute, string elementName, string elementKey)
        {
            _attributeName = attribute;
            _elementName = elementName;
            _elementKey = elementKey;
        }
    
    }
}
