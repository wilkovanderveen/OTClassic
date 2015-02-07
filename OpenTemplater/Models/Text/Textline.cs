using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using iTextSharp.text;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Collections;

namespace OpenTemplater.Models.Text
{
    public class Textline : IEnumerable<TextElement>
    {
        private TextElementCollection _textElements;
        private Unit _width;
        private Unit _height;
        private Paragraph _paragraph;
   
        private Paragraph.AlignmentType _aligment;

        public int Count
        {
            get { return _textElements.Count(); }
        }

        public Paragraph.AlignmentType Alignment
        {
            get { return _aligment; }
        }

        public Unit Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public Unit Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public Paragraph Paragraph
        {
            get { return _paragraph;  }
            set { _paragraph = value; }
        }

        /// <summary>
        /// If set to true, this textline is the last textline of the paragraph containing the textline.
        /// </summary>
        public bool EndsParagraph { get; set; }

        public Textline(Paragraph.AlignmentType alignmentType)
        {
            _height = new Unit(0);
            _aligment = alignmentType;
            _textElements = new TextElementCollection();
        }

        /// <summary>
        /// Creates a textline with the given paragraph as a parent.
        /// </summary>
        /// <param name="parent"></param>
        public Textline(Paragraph parent)
        {
            _height = new Unit(0);
            _aligment = parent.Alignment;
            _textElements = new TextElementCollection();
            _paragraph = parent;
        }

        public void Add(TextElement textElement)
        {
            _textElements.Add(textElement);
        }

        #region IEnumerable<TextElement> Members

        IEnumerator<TextElement> IEnumerable<TextElement>.GetEnumerator()
        {
            return _textElements.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _textElements.GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("{Text = \"");
            foreach (TextElement textElement in this._textElements)
            {
                sb.Append(textElement.Text);
            }
            sb.Append("\"}");
            return sb.ToString();
        }
    }
}