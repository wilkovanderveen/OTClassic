using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Exceptions
{
    public class KeyNotFoundException : BaseException
    {
        public KeyNotFoundException(string key)
        {
            SetMessage("Refenced element with key " + key + " not found.");
        }
    }
}