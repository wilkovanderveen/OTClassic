using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater
{
    public interface IRenderElementContainer : IRenderElement
    {
        IList<IRenderElement> Elements { get; }
    }
}
