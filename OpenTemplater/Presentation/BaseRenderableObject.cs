using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Presentation
{
    public abstract class BaseRenderableObject : IRenderableObject
    {
        private IList<IListener> _listeners;

        protected BaseRenderableObject()
        {
            _listeners = new List<IListener>();
        }

        public void Render()
        {
            BeginRender();
            RenderObject();
            EndRender();
        }

        private void BeginRender()
        {
            foreach(IListener listener in _listeners)
            {
                listener.Register(this);
            }
        }

        private void EndRender()
        {
            foreach (IListener listener in _listeners)
            {
                listener.Unregister(this);
            }
        }

        public abstract void RenderObject();

    }
}
