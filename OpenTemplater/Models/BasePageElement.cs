using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Data.Xml.Layout;

namespace OpenTemplater.Models
{
    public abstract class BasePageElement : BaseElement, IPageElement
    {
        protected BasePageElement(string key, IElementContainer parent) : base(key, parent)
        {
        }

        protected void ParseLayout(XmlLayoutDefinition xmlLayout)
        {
            layoutContainer = new Layout.LayoutContainer(this, xmlLayout);
        }

    }
}
