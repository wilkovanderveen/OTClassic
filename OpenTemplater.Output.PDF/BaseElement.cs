using iTextSharp.text.pdf;
using OpenTemplater.Models;
using OpenTemplater.Presentation;

namespace OpenTemplater.Output.PDF
{
    public abstract class BaseElement<T> : BaseRenderableObject where T : class, IPageElement
    {
        private readonly PdfDocument _document;
        private readonly T _element;
        private readonly PdfContentByte _pdfContent;

        protected BaseElement(PdfContentByte pdfContent, PdfDocument document, IPageElement element)
        {
            _pdfContent = pdfContent;
            _document = document;
            _element = element as T;
        }

        protected PdfContentByte PdfContent
        {
            get { return _pdfContent; }
        }

        protected PdfDocument Document
        {
            get { return _document; }
        }

        protected T Element
        {
            get { return _element; }
        }

        public override void RenderObject()
        {
            ;
        }
    }
}