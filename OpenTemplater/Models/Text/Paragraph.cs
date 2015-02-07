using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Common.Measuring.Text;
using OpenTemplater.Core.Modules;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Interfaces;

namespace OpenTemplater.Models.Text
{
    public class Paragraph
    {
        #region enumerations

        /// <summary>
        /// Paragraph text alignment type.
        /// </summary>
        public enum AlignmentType
        {
            left =0,
            center = 1,
            right = 2,
            justify = 3
        }

        #endregion

        #region private members

        private AlignmentType _alignment;
        private Unit _leading;
        private Text _text;

        private int _textLines;
        private float _textHeight;
        private Collections.TextElementCollection _textElements = new Collections.TextElementCollection();
        private Collections.TextlineCollection _textlines = new TextlineCollection();
        private Unit _offset;
        private string _symbol;

        #endregion

        #region public properties

        public Unit Leading
        {
            get { return _leading; }
        }

        public Unit Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public String Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        public AlignmentType Alignment
        {
            get { return _alignment; }
        }

        public Text Text
        {
            get { return _text; }
        }

        public IPageDefinition Page
        {
            get { return _text.Container.Page; }
        }

        public Unit ContentHeight
        {
            get { return new Unit(_textHeight); }
        }

        public Collections.TextElementCollection TextElements
        {
            get { return _textElements; }
        }

        #endregion

        #region constructors

        public Paragraph(Text text, string leading, string alignment)
        {
            _alignment = (AlignmentType) Enum.Parse(typeof (AlignmentType), alignment);
            _text = text;
            _leading = new Unit(leading);
        }

        private Paragraph(Paragraph paragraph, TextlineCollection textlines)
        {
            _alignment = paragraph.Alignment;
            _text = paragraph.Text;

            _leading = paragraph.Leading;
            _textlines = textlines;
        }

        #endregion

        public void Add(TextElement textelement)
        {
            _textElements.Add(textelement);
        }

        private void AddTextlinesAtBeginning(TextlineCollection textlines)
        {
        }

        /// <summary>
        /// Updates the numbers of textlines in this paragraph.
        /// </summary>
        /// <param name="lines">The number of lines to set.</param>
        public void UpdateTextlines(int lines)
        {
            _textLines = lines;
        }

        public void UpdateTextHeight(float height)
        {
            _textHeight = height;
        }
    }
}