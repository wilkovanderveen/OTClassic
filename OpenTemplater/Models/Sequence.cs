using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Business.Interfaces;
using OpenTemplater.Data.Xml.Layout;

namespace OpenTemplater.Business
{
    public class Sequence
    {
        public IList<IPageDefinition> PageDefintions
        {
            get; set;
        }
    }
}
