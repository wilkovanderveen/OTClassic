using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Layout
{
    public class Dimension
    {
        public string Value;
        public string From;
        public Relation Relation;
        public ResizeOptions Resizing;

        public Dimension(System.Xml.XmlNode dimension)
        {
            Value = dimension.Attributes["value"].Value;

            if (dimension.SelectSingleNode("relation") != null)
            {
                Relation = new Relation(dimension.SelectSingleNode("relation"));
            }

            if (dimension.SelectSingleNode("resizing") != null)
            {
                Resizing = new ResizeOptions(dimension.SelectSingleNode("resizing"));
            }
        }
    }
}