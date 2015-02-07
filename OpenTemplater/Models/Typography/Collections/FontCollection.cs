using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Exceptions;

namespace OpenTemplater.Models.Typography.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public class FontCollection : IEnumerable<Font>
    {
        private readonly List<Font> _fonts = new List<Font>();

        public Font this[string key]
        {
            get
            {
                if (_fonts.Any(f => f.Key == key))
                {
                    return _fonts.Where(f => f.Key == key).First();
                }
                else
                {
                    throw new FontNotFoundException(key);
                }
            }
        }

        ///<summary>
        ///</summary>
        public int Count
        {
            get { return _fonts.Count; }
        }

        IEnumerator<Font> IEnumerable<Font>.GetEnumerator()
        {
            return _fonts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _fonts.GetEnumerator();
        }

        public void Add(Font font)
        {
            _fonts.Add(font);
        }
    }
}