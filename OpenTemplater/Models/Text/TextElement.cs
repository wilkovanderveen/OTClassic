using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Common.Measuring.Text;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Typography;

namespace OpenTemplater.Models.Text
{
    public class TextElement
    {
        #region private members

        private readonly Color _color;
        private readonly Unit _fontSize;
        private readonly FontStyle _fontStyle;
        private readonly IPageDefinition _page;
        private readonly Paragraph _paragraph;
        private readonly string _text;
        private Boolean _causedOverflow;
        private Unit _charSpacing;
        private IEnumerable<Formatter> _formatters;

        #endregion

        #region public properties

        /// <summary>
        /// The actual textual contents of the textelement.
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        public FontStyle FontStyle
        {
            get { return _fontStyle; }
        }

        public Color Color
        {
            get { return _color; }
        }

        public Unit FontSize
        {
            get { return _fontSize; }
        }

        public Unit CharSpacing
        {
            set
            {
                if (value.HasEmValue)
                {
                    _charSpacing = value;
                }
                else
                {
                    throw new NotSupportedException("Only EMs are supported for this property");
                }
            }
            get { return _charSpacing; }
        }

        public bool CausedOverflow
        {
            get { return _causedOverflow; }
            set { _causedOverflow = value; }
        }

        public Text Parent
        {
            get { return _paragraph.Text; }
        }

        #endregion

        ///<summary>
        ///</summary>
        ///<param name="paragraph"></param>
        ///<param name="text"></param>
        ///<param name="font"></param>
        ///<param name="style"></param>
        ///<param name="fontsize"></param>
        ///<param name="color"></param>
        ///<exception cref="ArgumentException"></exception>
        public TextElement(Paragraph paragraph, string text, string font, string style, string fontsize, string color)
        {
            _page = paragraph.Page;

            _paragraph = paragraph;
            _text = text;
            _fontStyle = _page.Document.Fonts[font].Styles[style];
            _color = _page.Document.Colors[color];


            _fontSize = new Unit(fontsize);

            if (_fontSize.Points <= 0)
                throw new ArgumentException("Fontsize should be larger than zero.", "fontsize");
        }

        ///<summary>
        ///</summary>
        ///<param name="paragraph"></param>
        ///<param name="text"></param>
        ///<param name="font"></param>
        ///<param name="fontsize"></param>
        ///<param name="color"></param>
        public TextElement(Paragraph paragraph, string text, FontStyle font, Unit fontsize, Color color)
        {
            _page = paragraph.Page;
            _paragraph = paragraph;
            _text = text;

            _fontStyle = font;
            _color = color;
            _fontSize = fontsize;
        }

        ///<summary>
        ///</summary>
        ///<param name="splitCharacters"></param>
        ///<returns></returns>
        public TextElementCollection Split(SplitCharacterList splitCharacters)
        {
            var textElementCollection = new TextElementCollection();
            String[] elementStrings = Regex.Split(_text, @"(?<=[ .,;])");
            foreach (String elementString in elementStrings)
            {
                // todo: determinate if the splitcharacter should be kept.
                var textElement = new TextElement(_paragraph, elementString, _fontStyle, _fontSize, _color);
                textElementCollection.Add(textElement);
            }
            return textElementCollection;
        }
    }
}