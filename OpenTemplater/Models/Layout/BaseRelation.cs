using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;

namespace OpenTemplater.Models.Layout
{
    public abstract class BaseRelation
    {
        #region private members

        private IPageElement _element;
        private string _from;
        private string _key;

        #endregion

        public BaseRelation(IPageElement element, string key, string from)
        {
            if (element.Parent.HasElement(key))
            {
                _element = element.Parent[key];
                _from = from;
            }
        }

        public BaseRelation(string key, string from)
        {
            _key = key;
            _from = from;
        }

        /// <summary>
        /// Marks from which side of the element the relation is mapped.
        /// </summary>
        public string From
        {
            get { return _from; }
        }

        public IElement Element
        {
            get { return _element; }
        }
    }
}