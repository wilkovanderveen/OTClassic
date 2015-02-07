using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Data.Xml;

namespace OpenTemplater.Data.Xml.Layout
{
    public class XmlLayoutDefinition
    {
        public Dimension Width;
        public Dimension Height;
        public Dimension HOffset;
        public Dimension VOffset;

        public XmlLayoutDefinition(System.Xml.XmlNode layoutNode)
        {
            Width = new Dimension(layoutNode.SelectSingleNode("width"));
            Height = new Dimension(layoutNode.SelectSingleNode("height"));
            HOffset = new Dimension(layoutNode.SelectSingleNode("hoffset"));
            VOffset = new Dimension(layoutNode.SelectSingleNode("voffset"));
        }
    }
}