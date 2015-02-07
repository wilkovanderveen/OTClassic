using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Common.Exceptions
{
    public class MissingAttributeException : Exception
    {
        private string _attributeName;
        private string _nodeName;

        public override string Message
        {
            get { return "Required attribute " + _attributeName + " is missing in node " + _nodeName; }
        }

        public MissingAttributeException(string attribute, string node)
        {
            _attributeName = attribute;
            _nodeName = node;
        }
    }
}