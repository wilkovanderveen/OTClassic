using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Collections
{
    public interface IIndexableCollection<T> : IEnumerable<T>
    {
        T this[string key] { get; }
        Boolean HasKey(string key);
        void Add(T item);
    }
}