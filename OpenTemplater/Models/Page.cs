using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Interfaces;

namespace OpenTemplater.Models
{
    public class Page : BasePage
    {
        private int _pageNumber;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
        }

        public Page(Document document, String key, String width, String height, String colorType, String bleeding, int pageNumber)
            : base(document, key, width, height, colorType, bleeding)
        {
            _pageNumber = pageNumber;
        }

        public Page(IPageDefinition original, ElementCollection elements, int pageNumber)
            : this(
                original.Document, original.Key, original.Width.ToString(), original.Height.ToString(),
                original.ColorType, original.Bleeding.ToString(), pageNumber)
        {
            _contents = new Content(this, elements);
        }

        public static Page FromTemplate(PageTemplate template, ElementCollection elementCollection, int pageNumber)
        {
            return new Page(template, elementCollection, pageNumber);
        }
    }
}