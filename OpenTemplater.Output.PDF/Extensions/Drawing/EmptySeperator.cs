using iTextSharp.text.pdf.draw;

namespace OpenTemplater.Output.PDF.Extensions.Drawing
{
    class EmptySeperator : IDrawInterface
    {
        #region IDrawInterface Members

        public void Draw(iTextSharp.text.pdf.PdfContentByte canvas, float llx, float lly, float urx, float ury, float y)
        {
            //
        }

        #endregion
    }
}
