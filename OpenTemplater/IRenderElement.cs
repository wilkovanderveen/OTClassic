using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater
{
    public interface IRenderElement
    {
        void OnBeginRender();
        void Render();
        void OnRenderComplete();
    }
}
