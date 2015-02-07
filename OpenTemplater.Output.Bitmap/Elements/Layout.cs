using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenTemplater.Presentation.Bitmap.Elements
{
    public class Layout
    {
        private Models.Layout.LayoutContainer _businessLayoutContainer;
        private Size _size;
        private Point _topLeft;

        public Size Size
        {
            get { return _size; }
        }

        public Point TopLeftPoint
        {
            get { return _topLeft; }
        }

        public Layout(Models.Layout.LayoutContainer businessLayoutContainer)
        {
            int width = Convert.ToInt32(businessLayoutContainer.Layout.Width.Points);
            int height = Convert.ToInt32(businessLayoutContainer.Layout.Height.Points);

            _size = new Size(width, height);

            int left = Convert.ToInt32(businessLayoutContainer.Layout.Left.Points);
            int top = Convert.ToInt32(businessLayoutContainer.Layout.Top.Points);

            _topLeft = new Point(left, top);
        }
    }
}