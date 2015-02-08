using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Output.Bitmap;

namespace OpenTemplater.Presentation.Bitmap
{
    public class Document
    {
        private List<Page> _pages = new List<Page>();

        public Document(Models.Document documentTemplate, string outputFile)
        {
            foreach (Models.Page bPage in documentTemplate.Pages)
            {
                Page pPage = new Page(bPage);
            }
        }
    }
}