using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;

namespace OpenTemplater.Models.Layout
{
    /// <summary>
    /// The PageTemplateLayout class defines a layout for a page template.
    /// </summary>
    public class PageTemplateLayout
    {
        private Unit _horizontalOffset;
        private Unit _verticalOffset;
        private Unit _width;
        private Unit _height;

        private float _pageWidth;
        private float _pageHeight;

        /// <summary>
        /// CalculatedWidth of the layout.
        /// </summary>
        public Unit Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Height of the layout.
        /// </summary>
        public Unit Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Horizontal offset (X-Position) of the layout.
        /// </summary>
        public Unit HOffset
        {
            get { return _horizontalOffset; }
        }

        /// <summary>
        /// Vertical offset (Y-Position) of the layout.
        /// </summary>
        public Unit VOffset
        {
            get { return _verticalOffset; }
        }

        public PageTemplateLayout(string pageWidth, string pageHeight, string horizontalOffset, string verticalOffset,
                                  string width, string height)
        {
            _pageWidth = new Unit(pageWidth).Points;
            _pageHeight = new Unit(pageHeight).Points;
            _horizontalOffset = new Unit(horizontalOffset);
            _verticalOffset = new Unit(verticalOffset);
            _width = new Unit(width);
            _height = new Unit(height);
        }

        public PageTemplateLayout(PageTemplate template, string horizontalOffset, string verticalOffset, string width,
                                  string height)
            : this(
                template.Width.ToString(), template.Height.ToString(), horizontalOffset, verticalOffset, width, height)
        {
        }
    }
}