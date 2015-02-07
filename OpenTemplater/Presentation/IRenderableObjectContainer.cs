using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Presentation
{
    public interface IRenderableObjectContainer : IRenderableObject
    {
        void RenderChildren();
    }
}
