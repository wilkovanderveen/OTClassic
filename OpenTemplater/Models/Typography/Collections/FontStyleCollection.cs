using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Typography.Collections
{
    public class FontStyleCollection : IEnumerable<FontStyle>
    {
        private List<FontStyle> _fontStyles = new List<FontStyle>();

        public FontStyle this[string key]
        {
            get
            {
                var returnValue = from fontstyle in _fontStyles where fontstyle.Key == key select fontstyle;
                return returnValue.First<FontStyle>();
            }
        }

        IEnumerator<FontStyle> IEnumerable<FontStyle>.GetEnumerator()
        {
            return _fontStyles.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _fontStyles.GetEnumerator();
        }

        public void Add(FontStyle fontStyle)
        {
            _fontStyles.Add(fontStyle);
        }
    }
}