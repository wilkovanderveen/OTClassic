using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Data.Xml.Layout;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;
using Dimension = OpenTemplater.Models.Layout.Dimension;

namespace OpenTemplater.Models
{
    /// <summary>
    /// Provides a base for page elements.
    /// </summary>
    public abstract class BaseElement : BaseModel
    {
        
        private UInt16 _zOrder;
        protected Layout.LayoutContainer layoutContainer;
        private Layout.Dimension _contentDimension = new Dimension(0, 0);
        private IElementContainer _parent;
        private Content _container;
        private IDictionary<string, IElement> _releatedElements = new Dictionary<string, IElement>();

        public void SetLayout()
        {
            this.LayoutContainer.SetLayout();
        }

        public IDictionary<string, IElement> RelatedElements
        {
            get { return _releatedElements; }
        }

        /// <summary>
        /// Positive integer value representing the zorder for this element.
        /// </summary>
        public UInt16 ZOrder
        {
            get { return _zOrder; }
            set { _zOrder = value; }
        }

        public Content Container
        {
            get { return _container; }

            set { _container = value; }
        }

        /// <summary>
        /// XmlLayoutDefinition object defining the layout for this element.
        /// </summary>
        public Layout.LayoutContainer LayoutContainer
        {
            get { return layoutContainer; }

            set { layoutContainer = value; }
        }

        public LayoutObject Layout
        {
            get { return layoutContainer.Layout; }
        }

        public Layout.Dimension ContentDimension
        {
            get { return _contentDimension; }
            set { _contentDimension = value; }
        }

        public IElementContainer Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

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

        protected BaseElement(string key, IElementContainer parent) : base(key)
        {
            _parent = parent;
        }
        
        public override string ToString()
        {
            return this.GetType().DeclaringType.Name + " {}";
        }
    }
}