using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Exceptions;
using KeyNotFoundException=System.Collections.Generic.KeyNotFoundException;

namespace OpenTemplater.Models.Collections
{
    public class ElementCollection : IIndexableCollection<IPageElement>
    {
        #region private members

        private Dictionary<string, IPageElement> _elements = new Dictionary<string, IPageElement>();
        private Content _content;

        #endregion

        /// <summary>
        /// Gets an element from this collection referenced by the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IPageElement this[string key]
        {
            get
            {
                if (HasKey(key))
                {
                    return _elements[key];
                }
                else
                {
                    string exceptiontext = "The specified key {" + key + "} was not found";

                    if (_content != null)
                    {
                        if (_content.Parent != null)
                        {
                            exceptiontext += " in element with key {" + _content.Parent.Key + "}";
                        }
                        else
                        {
                            if (_content.Page != null)
                            {
                                exceptiontext += " in page with key {" + _content.Page.Key + "}";
                            }
                        }
                    }
                    throw new KeyNotFoundException(exceptiontext + ".");
                }
            }
        }

        /// <summary>
        /// Determinates if this collection has an element with the specified key.
        /// </summary>
        /// <param name="key">String which represents the key to look for in the collection.</param>
        /// <returns></returns>
        public Boolean HasKey(string key)
        {
            return _elements.ContainsKey(key);
        }

        public ElementCollection(Content content)
        {
            _content = content;
        }

        public ElementCollection(Content content, IEnumerable<IPageElement> elements) : this(content)
        {
            foreach (IPageElement e in elements)
            {
                _elements.Add(e.Key, e);
            }
        }

        public List<IPageElement> ToList()
        {
            return _elements.Values.ToList();
        }

        public void Add(IPageElement element)
        {
            if (!_elements.ContainsKey(element.Key))
            {
                _elements.Add(element.Key, element);
            }
            else
            {
                throw new DuplicateKeyException(element);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #region IEnumerable<IElement> Members

        IEnumerator<IPageElement> IEnumerable<IPageElement>.GetEnumerator()
        {
            return new List<IPageElement>(_elements.Values).GetEnumerator();
        }

        #endregion
    }
}