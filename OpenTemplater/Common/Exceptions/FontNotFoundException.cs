using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Common.Exceptions
{
    ///<summary>
    ///</summary>
    public class FontNotFoundException : Exception
    {
        private readonly string _fontKey;
        private string _styleKey;

        ///<summary>
        ///</summary>
        ///<param name="fontKey"></param>
        public FontNotFoundException(string fontKey)
        {
            _fontKey = fontKey;
        }

        public override string Message
        {
            get { return "No font defined with supplied key: " + _fontKey; }
        }
    }
}