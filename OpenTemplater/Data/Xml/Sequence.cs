using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenTemplater.Data.Xml.Layout;

namespace OpenTemplater.Data.Xml
{
    public class Sequence
    {
        public List<PagingTemplate> Pages;
       

        public Sequence(XmlNode sequenceNode)
        {
            Pages = new List<PagingTemplate>();
            foreach(XmlNode pagingNode in sequenceNode.SelectNodes("page"))
            {
                Pages.Add(new PagingTemplate(pagingNode));
            }
        }
    }
}
