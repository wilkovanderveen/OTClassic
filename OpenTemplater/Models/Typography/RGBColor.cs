using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Typography
{
    public class RGBColor : Interfaces.IColorType
    {
        private byte _red;
        private byte _green;
        private byte _blue;
        private byte _alpha;

        /// <summary>
        /// The red component of a RGB color.
        /// </summary>
        public byte Red
        {
            get { return _red; }
        }

        public byte Green
        {
            get { return _green; }
        }

        public byte Blue
        {
            get { return _blue; }
        }

        public byte Alpha
        {
            get { return _alpha; }
        }

        public RGBColor(string red, string green, string blue)
        {
            _red = System.Convert.ToByte(red);
            _green = System.Convert.ToByte(green);
            _blue = System.Convert.ToByte(blue);
        }
    }
}