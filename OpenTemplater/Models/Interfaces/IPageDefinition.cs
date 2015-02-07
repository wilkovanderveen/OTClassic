using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;

namespace OpenTemplater.Models.Interfaces
{
    public interface IPageDefinition : IElementContainer
    {
        /// <summary>
        /// Document to which this page definition belongs.
        /// </summary>
        Document Document { get; }

        /// <summary>
        /// Height of this page definition.
        /// </summary>
        Unit Height { get; }

        Unit Width { get; }
        String ColorType { get; }
        Unit Bleeding { get; }
    }
}