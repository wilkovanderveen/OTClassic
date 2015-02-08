using System;
using OpenTemplater.Models.Typography.Interfaces;

namespace OpenTemplater.Models.Typography
{
    public class RGBColor : IColorType
    {
        public RGBColor(string red, string green, string blue, string alpha)
        {
            Red = Convert.ToByte(red);
            Green = Convert.ToByte(green);
            Blue = Convert.ToByte(blue);
            Alpha = Convert.ToByte(alpha);
        }

        /// <summary>
        ///     The red component of a RGB color.
        /// </summary>
        public byte Red { get; private set; }

        public byte Green { get; private set; }

        public byte Blue { get; private set; }

        public byte Alpha { get; private set; }
    }
}