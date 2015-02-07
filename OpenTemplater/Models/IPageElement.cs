using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;

namespace OpenTemplater.Models
{
    public interface IPageElement : IElement
    {
        IDictionary<String, IElement> RelatedElements { get; }
        UInt16 ZOrder { get; set; }
        IElementContainer Parent { get; }
        LayoutContainer LayoutContainer { get; }
        void SetLayout();
    }
}