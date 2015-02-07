using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater
{
    public abstract class BaseRenderElementContainer : BaseRenderElement, IRenderElementContainer
    {
        private IList<IRenderElement> _elements;

        public IList<IRenderElement> Elements
        {
            get { return _elements; }
        }

        public BaseRenderElementContainer()
        {
            _elements = new List<IRenderElement>();
        }

        public override void RenderContents()
        {
           foreach(IRenderElement element in _elements)
           {
               element.Render();
           }
        }
    }
}
