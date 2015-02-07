using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTemplater.Output.PDF.Typography
{
    public class FontStyleList : List<FontStyle>
    {
        public FontStyle this[string key]
        {
            get
            {
                try
                {
                    var returnValue = from FontStyle f in this where f.Key == key select f;
                    return returnValue.First<FontStyle>();
                }
                catch (Exception)
                {
                    throw new KeyNotFoundException(key);
                }
            }
        }
    }
}