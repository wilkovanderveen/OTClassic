using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Collections
{
    public class PageCollection : IEnumerable<Page>
    {
        private List<Page> _pages = new List<Page>();

        public Page this[int index]
        {
            get { return _pages[index]; }
        }

        public Page this[string key]
        {
            get { return _pages.Where(p => p.Key == key).FirstOrDefault(); }
        }

        public void Add(Page page)
        {
            _pages.Add(page);
        }

        public int Count
        {
            get { return _pages.Count; }
        }

        IEnumerator<Page> IEnumerable<Page>.GetEnumerator()
        {
            return _pages.GetEnumerator();
        }

        public void AddRange(IEnumerable<Page> pages)
        {
            _pages.AddRange(pages);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _pages.GetEnumerator();
        }
    }
}