using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Presentation.PDF.Typography
{
    public class Color
    {
        private string _key;

        public string Key
        {
            get { return _key; }
        }

        public iTextSharp.text.pdf.CMYKColor CMYKColor;
        public iTextSharp.text.Color RGBColor;
        public iTextSharp.text.pdf.PdfSpotColor PMSColor;

        /// <summary>
        /// Constructs an PDF Color object from a business color.
        /// </summary>
        /// <param name="bColor">Business color to use as base.</param>
        public Color(Models.Typography.Color bColor)
        {
            _key = bColor.Key;
            CMYKColor = new iTextSharp.text.pdf.CMYKColor(bColor.CMYKColor.Cyan, bColor.CMYKColor.Magenta,
                                                          bColor.CMYKColor.Yellow, bColor.CMYKColor.Black);
            RGBColor = new iTextSharp.text.Color(System.Convert.ToInt32(bColor.RGBColor.Red),
                                                 System.Convert.ToInt32(bColor.RGBColor.Green),
                                                 System.Convert.ToInt32(bColor.RGBColor.Blue));

            if (bColor.HasPMSColor)
            {
                // TODO: Implement tint for PMS Colors.
                PMSColor = new iTextSharp.text.pdf.PdfSpotColor(bColor.PMSColor.Name, 1, RGBColor);
            }
        }
    }
}