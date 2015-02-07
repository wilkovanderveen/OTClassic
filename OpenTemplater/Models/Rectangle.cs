using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Core.Modules;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Typography;

namespace OpenTemplater.Models
{
    public class Rectangle : BaseElement, IPageElement, IElementContainer
    {
        private Unit _borderWidth;
        private Unit _roundness;
        private Color _borderColor;
        private Color _fillColor;
        private Content _content;

        public Unit BorderWidth
        {
            get { return _borderWidth; }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
        }

        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; }
        }

        public Unit Roundness
        {
            get { return _roundness; }
            set { _roundness = value; }
        }

        public Unit ContentHeight
        {
            get { throw new NotImplementedException(); }
        }

        public Rectangle(Content container, string key, string bordercolor, string borderwidth)
            : base(key, container.Parent)
        {
            Container = container;
            Key = key;
            _borderColor = Container.Page.Document.Colors[bordercolor];
            _borderWidth = new Unit(borderwidth);
        }

        #region IContainer Members

        public IElement this[string key]
        {
            get { return Contents.Elements[key]; }
        }

        #endregion

        #region IElementContainer Members

        public Content Contents
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        public bool HasElement(string key)
        {
            return _content.Elements.HasKey(key);
        }

        public void Relocate()
        {
            foreach (IPageElement element in this.Contents.Elements)
            {


                element.Layout.Top = this.Layout.Top;
            }
        }

        #endregion

        #region IIndexableCollection<IPageElement> Members

        IPageElement IIndexableCollection<IPageElement>.this[string key]
        {
            get { return _content.Elements[key]; }
        }

        public bool HasKey(string key)
        {
            throw new NotImplementedException();
        }

        public void Add(IPageElement item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<IPageElement> Members

        public IEnumerator<IPageElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}