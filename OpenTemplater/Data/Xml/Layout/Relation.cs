using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Layout
{
    public class Relation
    {
        public String Element;
        public String From;

        public Relation(System.Xml.XmlNode relationNode)
        {
            if (relationNode.Attributes["element"] != null)
            {
                Element = relationNode.Attributes["element"].Value;
            }
            From = relationNode.Attributes["from"].Value;
        }
    }
}