using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data
{
    public interface IXmlPageDefinition
    {
        string Width { get; }
        string Height { get; }
        string Colortype { get; }
        string Bleeding { get; }
        string Key { get; }
    }
}