using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Interfaces;

namespace OpenTemplater.Models
{
    /// <summary>
    /// Defines a collection with page elements which can be used as content for pages and content containers.
    /// </summary>
    public class Content : IIndexableCollection<IPageElement>
    {
        private IIndexableCollection<IPageElement> _elements;
        private IElementContainer _parent;

        /// <summary>
        /// Indexer to get an element by key for this content object.
        /// </summary>
        /// <param name="key">Key for the element to get.</param>
        /// <returns>Element.</returns>
        public IPageElement this[string key]
        {
            get { return _elements[key]; }
        }

        public Boolean HasElement(string key)
        {
            return _elements.HasKey(key);
        }

        #region public properties

        public IElementContainer Parent
        {
            get { return _parent; }
        }

        public Unit Height { get; set; }

        public IPageDefinition Page
        {
            get
            {
                if ((Parent as IPageDefinition) == null)
                {
                    return (Parent as IPageElement).Page;
                }
                return Parent as IPageDefinition;
            }
        }


        public IIndexableCollection<IPageElement> Elements
        {
            get { return _elements; }
        }

        #endregion

        #region constructors

        public Content(IElementContainer container)
        {
            _parent = container;
            _elements = new ElementCollection(this);
        }


        public Content(IElementContainer container, ElementCollection elementCollection)
        {
            _parent = container;
            _elements = elementCollection;

            UpdateContentHeight();
        }

        #endregion

        #region IIndexableCollection<IPageElement> Members

        public bool HasKey(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds an page element to this content.
        /// </summary>
        /// <param name="item">Element which implements the IPageElement interface.</param>
        public void Add(IPageElement item)
        {
            this._elements.Add(item);
        }

        #endregion

        #region IEnumerable<IPageElement> Members

        public IEnumerator<IPageElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void UpdateContentHeight()
        {
            float topPosition = Elements.Max(el => el.Layout.Top).Points;
            float bottomPosition = Math.Max(Elements.Min(el => el.Layout.Bottom).Points, 0);
            float height = topPosition - bottomPosition;

           

            this.Height = new Unit(height);

           
        }

        #endregion
    }
}