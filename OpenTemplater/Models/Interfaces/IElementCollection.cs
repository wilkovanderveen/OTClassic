using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Business.Interfaces
{
    public interface IElementCollection<T> : IEnumerable<T> where T : ITextElement
    {
        void AddRange(IElementCollection<T> elementCollection);
        void Clear();
        void Remove(T element);
    }
}
