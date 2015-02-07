namespace OpenTemplater.Output.PDF
{
    public interface IPdfPageObserver
    {
        void Update(PdfPage pdfPage);
    }
}
