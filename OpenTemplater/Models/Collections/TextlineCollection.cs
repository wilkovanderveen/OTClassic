using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Text;

namespace OpenTemplater.Models.Collections
{
    public class TextlineCollection : ICollection<Textline>
    {
        private List<Textline> _textlines;
        private float _height;

        public int Count
        {
            get { return _textlines.Count; }
        }

        public Unit Height
        {
            get { return new Unit(_height); }
        }

        public Unit Width
        {
            get { return new Unit(_textlines.Count > 0 ? _textlines.Max(tl => tl.Width.Points) : 0); }
        }

        public TextlineCollection()
        {
            _textlines = new List<Textline>();
        }

        public TextlineCollection(List<Textline> textlines)
        {
            _textlines = textlines;
            _height = textlines.Sum(tl => tl.Height.Points);
        }

        public void Add(Textline textline)
        {
            _textlines.Add(textline);
            _height += textline.Height.Points;
        }

        public void AddRange(TextlineCollection textlineCollection)
        {
            _textlines.AddRange(textlineCollection.GetTextlines());
            _height += textlineCollection.Height.Points;
        }

        public IEnumerator<Textline> GetEnumerator()
        {
            return _textlines.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _textlines.GetEnumerator();
        }

        public List<Textline> GetTextlines()
        {
            return _textlines;
        }

        #region ICollection<Textline> Members

        public void Clear()
        {
            _textlines.Clear();
        }

        public bool Contains(Textline item)
        {
            return _textlines.Contains(item);
        }

        public void CopyTo(Textline[] array, int arrayIndex)
        {
            _textlines.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Textline item)
        {
            return _textlines.Remove(item);
        }

        #endregion
    }
}