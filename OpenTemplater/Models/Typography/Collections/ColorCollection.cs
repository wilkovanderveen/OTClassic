using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Typography.Collections
{
    /// <summary>
    /// Represents a collection of color objects, usable for templates.
    /// </summary>
    public class ColorCollection : IEnumerable<Color>
    {
        private IDictionary<String, Color> _colors = new Dictionary<String, Color>();

        public Color this[string key]
        {
            get
            {
                return _colors[key];
            }
        }

        public int Count
        {
            get { return _colors.Count; }
        }

        public void Add(Color color)
        {
            _colors.Add(color.Key, color);
        }

        #region IEnumerable<Color> Members

        public IEnumerator<Color> GetEnumerator()
        {
            return _colors.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _colors.Values.GetEnumerator();
        }

        #endregion
    }
}