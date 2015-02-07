using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;

namespace OpenTemplater.Models.Exceptions
{
    internal class DuplicateKeyException : BaseException
    {
        public DuplicateKeyException(IPageElement element)
        {
            SetMessage("Unable to add element with key " + element.Key + " in context " + element.Parent.Key + ".");
        }
    }
}