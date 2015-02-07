using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Presentation.PDF.Typography
{
    public class ColorList : List<Color>
    {
        public Color this[string key]
        {
            get
            {
                var returnValue = from Color c in this where c.Key == key select c;
                return returnValue.First<Color>();
            }
        }
    }
}