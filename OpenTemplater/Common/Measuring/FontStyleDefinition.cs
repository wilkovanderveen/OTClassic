namespace OpenTemplater.Common.Measuring
{
    public class FontStyleDefinition
    {
        private readonly string _key;
        private readonly iTextSharp.text.Font _styleFont;

        public string Key
        {
            get { return _key; }
        }

        public iTextSharp.text.Font Font
        {
            get { return _styleFont; }
        }

        public FontStyleDefinition(Models.Typography.FontStyle bFontStyle)
        {
            _styleFont = iTextSharp.text.FontFactory.GetFont(bFontStyle.Path, bFontStyle.Font.Encoding,
                                                             bFontStyle.Font.IsEmbedded,
                                                             bFontStyle.Font.DefaultFontSize.Points);
            _key = bFontStyle.Font.Key + "_" + bFontStyle.Key;
        }
    }
}