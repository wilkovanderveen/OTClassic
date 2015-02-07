using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Collections;
using OpenTemplater.Data.Xml;

namespace OpenTemplater.Models
{
    public class PageSequence : IEnumerable<PageSequenceElement>
    {
        private List<PageSequenceElement> _elements = new List<PageSequenceElement>();
        private Document _document;

        public void Add(PageSequenceElement page)
        {
            _elements.Add(page);
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        IEnumerator<PageSequenceElement> IEnumerable<PageSequenceElement>.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        public PageSequence(Document document)
        {
            _document = document;
        }

        public static PageSequence Parse(Document document, XmlDocumentDefinition xmlDocumentDefinition)
        {
            PageSequence returnValue = new PageSequence(document);

            foreach (XmlPageSequenceElement xmlPageSequenceElement in xmlDocumentDefinition.PageSequenceElements)
            {
                returnValue.Add(PageSequenceElement.Parse(xmlPageSequenceElement));
            }

            return returnValue;
        }
    }
}