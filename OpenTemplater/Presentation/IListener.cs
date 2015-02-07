using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Presentation
{
    public interface IListener
    {
        void Register(BaseRenderableObject obj);
        void Unregister(BaseRenderableObject obj);
    }
}
