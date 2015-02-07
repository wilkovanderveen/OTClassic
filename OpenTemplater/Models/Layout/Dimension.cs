using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;

namespace OpenTemplater.Models.Layout
{
    public class Dimension
    {
        private Unit _width;
        private Unit _height;

        public Unit Width
        {
            get { return _width; }
        }

        public Unit Height
        {
            get { return _height; }
        }

        public Dimension(string width, string height)
        {
            _width = new Unit(width);
            _height = new Unit(height);
        }

        public Dimension(float width, float height)
        {
            _width = new Unit(width);
            _height = new Unit(height);
        }

        public override string ToString()
        {
            StringBuilder layoutString = new StringBuilder();
            layoutString.Append(_width.ToString());
            layoutString.Append(_height.ToString());

            return layoutString.ToString();
        }
    }
}