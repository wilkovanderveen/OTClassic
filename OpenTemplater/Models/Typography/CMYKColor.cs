using System;
using System.Globalization;
using System.Text;
using OpenTemplater.Models.Typography.Interfaces;

namespace OpenTemplater.Models.Typography
{
    public class CMYKColor : IColorType
    {
        private float _black;
        private float _cyan;
        private float _magenta;
        private float _tint;
        private float _yellow;

        public CMYKColor(String cyan, String magenta, String yellow, String black)
        {
            SetProperties(cyan, magenta, yellow, black, null);
        }

        public CMYKColor(String cyan, String magenta, String yellow, String black, String tint)
        {
            SetProperties(cyan, magenta, yellow, black, tint);
        }

        public float Cyan
        {
            get { return _cyan; }
        }

        public float Magenta
        {
            get { return _magenta; }
        }

        public float Yellow
        {
            get { return _yellow; }
        }

        public float Black
        {
            get { return _black; }
        }

        public float Tint
        {
            get { return _tint; }
        }

        private void SetProperties(String cyan, String magenta, String yellow, String black, String tint)
        {
            var nfi = new NumberFormatInfo();
            nfi.CurrencyDecimalSeparator = ".";

            _cyan = Convert.ToSingle(cyan, nfi);
            _magenta = Convert.ToSingle(magenta, nfi);
            _yellow = Convert.ToSingle(yellow, nfi);
            _black = Convert.ToSingle(black, nfi);
            _tint = Convert.ToSingle(tint, nfi);
        }

        public override string ToString()
        {
            return String.Format("C={0}, M={1}, Y={2}, K={3}", new string[]
                                                                   {
                                                                       Cyan.ToString(),
                                                                       Magenta.ToString(),
                                                                       Yellow.ToString(),
                                                                       Black.ToString(),
                                                                   });
        }
    }
}