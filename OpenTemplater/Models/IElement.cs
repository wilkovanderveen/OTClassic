using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;

namespace OpenTemplater.Models
{
    public interface IElement
    {
        string Key { get; }

        /// <summary>
        /// Layout for this element. 
        /// </summary>
        LayoutObject Layout { get; }
        IPageDefinition Page { get; }
    }
}