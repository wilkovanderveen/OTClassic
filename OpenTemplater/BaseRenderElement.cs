using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater
{
    public abstract class BaseRenderElement : IRenderElement
    {

        public abstract void OnBeginRender();
        
        public void Render()
        {
            OnBeginRender();
            RenderContents();
            OnRenderComplete();
        }

        public abstract void RenderContents();
        public abstract void OnRenderComplete();
    }
}
