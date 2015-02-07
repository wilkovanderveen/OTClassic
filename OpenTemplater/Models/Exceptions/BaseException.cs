using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Exceptions
{
    public abstract class BaseException : System.Exception
    {
        private string _message;

        public override string Message
        {
            get { return _message; }
        }

        internal void SetMessage(string message)
        {
            _message = message;
        }
    }
}