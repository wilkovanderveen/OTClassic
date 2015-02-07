using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Presentation.PDF.Typography
{
    public class FontStyle
    {
        private string _key;
        private iTextSharp.text.Font _styleFont;

        public string Key
        {
            get { return _key; }
        }

        public iTextSharp.text.Font Font
        {
            get { return _styleFont; }
        }

        public FontStyle(Models.Typography.FontStyle bFontStyle)
        {
            _styleFont = iTextSharp.text.FontFactory.GetFont(bFontStyle.Path, bFontStyle.Font.Encoding,
                                                             bFontStyle.Font.IsEmbedded,
                                                             bFontStyle.Font.DefaultFontSize.Points);
            _key = bFontStyle.Font.Key + "_" + bFontStyle.Key;
        }
    }
}