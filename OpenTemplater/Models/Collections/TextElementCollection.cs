using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Text;

namespace OpenTemplater.Models.Collections
{
    public class TextElementCollection : IEnumerable<TextElement>
    {
        private List<Text.TextElement> _textElements = new List<TextElement>();

        public void Add(Text.TextElement textelement)
        {
            _textElements.Add(textelement);
        }

        public void AddRange(IEnumerable<TextElement> textElements)
        {
            _textElements.AddRange(textElements);
        }

        public IEnumerator<TextElement> GetEnumerator()
        {
            return _textElements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _textElements.GetEnumerator();
        }
    }
}