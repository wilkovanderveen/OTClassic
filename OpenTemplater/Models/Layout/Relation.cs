using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;

namespace OpenTemplater.Models.Layout
{
    public class Relation : BaseRelation
    {
        public Relation(IPageElement element, string key, string from) : base(element, key, from)
        {
        }

        public Relation(string key, string from) : base(key, from)
        {
        }
    }
}