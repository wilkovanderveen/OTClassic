using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater
{
    public interface IRenderEngine
    {
        void Render(string uri, string outputFile);
        void Render(string xmlUri, string xslUri, string outputFile);
        void Render(Models.Document document, string outputFile);
    }
}
