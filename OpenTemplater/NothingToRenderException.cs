using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater
{
    public class NothingToRenderException : Exception
    {
        private string _message;

        public NothingToRenderException(string subMessage)
        {
            _message = subMessage;
        }

        public override string Message
        {
            get { return string.Format("Nothing to render: {0}.", _message); }
        }
    }
}
