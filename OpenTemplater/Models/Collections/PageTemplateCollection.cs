using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Collections
{
    public class PageTemplateCollection : IEnumerable<PageTemplate>
    {
        private List<PageTemplate> _pageTemplates = new List<PageTemplate>();

        public PageTemplate this[int index]
        {
            get { return _pageTemplates[index]; }
        }

        public PageTemplate this[string key]
        {
            get { return _pageTemplates.Where(pt => pt.Key == key).FirstOrDefault(); }
        }

        public void Add(PageTemplate page)
        {
            _pageTemplates.Add(page);
        }

        public int Count
        {
            get { return _pageTemplates.Count; }
        }

        IEnumerator<PageTemplate> IEnumerable<PageTemplate>.GetEnumerator()
        {
            return _pageTemplates.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _pageTemplates.GetEnumerator();
        }
    }
}