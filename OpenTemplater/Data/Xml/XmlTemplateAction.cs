using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenTemplater.Data.Xml
{
    public class XmlTemplateAction
    {
        public string ActionName;


        public XmlTemplateAction(XmlNode xmlActionNode)
        {
            ActionName = xmlActionNode.Name;
        }
    }
}