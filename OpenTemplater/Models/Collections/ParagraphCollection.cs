using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Text;

namespace OpenTemplater.Models.Collections
{
    public class ParagraphCollection : IEnumerable<Paragraph>
    {
        private List<Paragraph> _paragraphs = new List<Paragraph>();

        public IEnumerator<Paragraph> GetEnumerator()
        {
            return _paragraphs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _paragraphs.GetEnumerator();
        }

        public void Add(Paragraph paragraph)
        {
            _paragraphs.Add(paragraph);
        }

        public void AddAsFirst(Text.Paragraph paragraph)
        {
            _paragraphs.Insert(0, paragraph);
        }
    }
}