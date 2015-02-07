using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;

namespace OpenTemplater.Models.Layout
{
    public class LayoutObject
    {
        public Unit Width { get; set; }
        public Unit Height { get; set; }
        public Unit Top { get; set; }

        public Unit Bottom
        {
            get
            {
                // Set value to reverted Y-Axis
                return new Unit(Top.Points - Height.Points);
            }
        }

        public Unit Left { get; set; }

        public Unit Right
        {
            get { return new Unit(Left.Points + Width.Points); }
        }

        public LayoutObject()
        {
        }

        public LayoutObject(Unit width, Unit height)
        {
            Top = new Unit(0);
            Left = new Unit(0);
            Width = width;
            Height = height;
        }
    }
}