using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Collections;

namespace OpenTemplater.Models
{
    public interface IElementContainer : IElement, IIndexableCollection<IPageElement>
    {
        Content Contents { get; set; }
        bool HasElement(string key);
    }
}