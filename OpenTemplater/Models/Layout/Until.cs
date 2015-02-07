using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;

namespace OpenTemplater.Models.Layout
{
    internal class Until : BaseRelation
    {
        public Until(IPageElement element, string key, string from) : base(element, key, from)
        {
        }

        public Until(string key, string from)
            : base(key, from)
        {
        }
    }
}