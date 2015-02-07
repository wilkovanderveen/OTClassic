using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;

namespace OpenTemplater.Models.Layout
{
    public class ResizeOptions
    {
        private Unit _maximum;
        private Unit _minimum;
        private ResizeType _resizeType;

        public Unit Maximum
        {
            get { return _maximum; }
            set { _maximum = value; }
        }

        public Unit Minimum
        {
            get { return _minimum; }
            set { _minimum = value; }
        }

        public ResizeType ResizeType
        {
            get { return _resizeType; }
        }


        public ResizeOptions(ResizeType resizeType)
        {
            _resizeType = resizeType;
        }
    }
}