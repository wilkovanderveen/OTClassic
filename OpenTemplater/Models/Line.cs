using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Typography;


namespace OpenTemplater.Models
{
    public class Line : BasePageElement
    {
        private Unit _width;
        private Color _color;

        public Unit Width
        {
            get { return _width; }
        }

        public Color Color
        {
            get { return _color; }
        }

        public Unit ContentHeight
        {
            get { throw new NotImplementedException(); }
        }

        public Line(Content container, string key, string width, string color) : base(key, container.Parent)
        {
            Container = container;
            Key = key;
            _width = new Unit(width);
            _color = Container.Page.Document.Colors[color];
        }


    }
}