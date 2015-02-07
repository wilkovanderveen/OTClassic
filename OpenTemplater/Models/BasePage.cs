using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;

namespace OpenTemplater.Models
{
    public abstract class BasePage : IPageDefinition
    {
        protected Content _contents;
        private String _key;
        private Unit _width;
        private Unit _height;
        private Unit _bleeding;
        private Document _document;
        private string _colorType;
        private LayoutObject _layout;

        public LayoutObject Layout
        {
            get { return _layout; }
        }

        public Document Document
        {
            get { return _document; }
        }

        public IPageDefinition Page
        {
            get { return this; }
        }

        public String Key
        {
            get { return _key; }
            internal set { _key = value; }
        }

        public Content Contents
        {
            get { return _contents; }

            set { _contents = value; }
        }

        public string ColorType
        {
            get { return _colorType; }
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

        public IPageElement this[string key]
        {
            get { return Contents[key]; }
        }

        public Unit Bleeding
        {
            get { return _bleeding; }
        }

        /// <summary>
        /// Constructs a page base on the document object this page belongs to. 
        /// </summary>
        /// <param name="document">Document object where this page belongs to.</param>
        /// <param name="key"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="colortype"></param>
        /// <param name="bleeding"></param>
        protected BasePage(Document document, string key, string width, string height, string colortype, string bleeding)
        {
            Contents = new Content(this);
            _document = document;
            _width = new Unit(width);
            _height = new Unit(height);
            _colorType = colortype;
            _key = key;
            _layout = new LayoutObject();

            if (!string.IsNullOrEmpty(bleeding))
            {
                _bleeding = new Unit(bleeding);
            }
            else
            {
                _bleeding = new Unit(0);
            }
        }

        protected void SetLayout(LayoutObject layout)
        {
            _layout = layout;
        }

        public void AddElement(IPageElement element)
        {
            _contents.Elements.Add(element);
        }

        #region IElementContainer Members

        public bool HasElement(string key)
        {
            return _contents.Elements.HasKey(key);
        }

        #endregion

        #region IIndexableCollection<IElement> Members

        public bool HasKey(string key)
        {
            throw new NotImplementedException();
        }

        public void Add(IElement item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<IElement> Members

        public IEnumerator<IElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        #endregion

        #region IIndexableCollection<IPageElement> Members

        /// <summary>
        /// Adds a new page element to the page contents.
        /// </summary>
        /// <param name="item">An element which implements the IPageElement interface.</param>
        public void Add(IPageElement item)
        {
           this.Contents.Add(item);
        }

        #endregion

        #region IEnumerable<IPageElement> Members

        IEnumerator<IPageElement> IEnumerable<IPageElement>.GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        #endregion
    }
}