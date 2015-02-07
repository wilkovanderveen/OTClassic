using System.Collections.Generic;
using System.Linq;
using OpenTemplater.Models;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;
using System;

namespace OpenTemplater.Core.Modules
{
    public class PagingModule
    {
        public IList<Page> GetPages(PageSequence pageSequence, PageCollection pageCollection,
                                    PageTemplateCollection pageTemplateCollection)
        {
            List<Page> pages = new List<Page>();
            int fillUpPageIndex=-1;

            // Add dynamic elements.
            foreach (PageSequenceElement pageSequenceElement in pageSequence)
            {
                if (pageSequenceElement.FillUpMultiplier == 0)
                {
                    if (pageSequenceElement.PageReferenceKey != null)
                    {
                        Page page = pageCollection[pageSequenceElement.PageReferenceKey];
                        if (page != null)
                            pages.Add(page);
                    }

                    if (pageSequenceElement.PageTemplateReferenceKey != null)
                    {
                        pages.AddRange(
                            CreatePagesFromTemplate(pageTemplateCollection[pageSequenceElement.PageTemplateReferenceKey]));
                    }
                }
                else
                {
                    fillUpPageIndex = pages.Count;
                }
            }

            // Also check if there are fillup templates.
            if (pageSequence.Any(pe => pe.FillUpMultiplier > 0))
            {
                int multiplier = pageSequence.Where(pe => pe.FillUpMultiplier > 0).FirstOrDefault().FillUpMultiplier;

                // Add fillup pages.
                int pagesToAdd = multiplier - (pages.Count % multiplier);
                if (pagesToAdd < multiplier)
                {
                    Page p = pageCollection[pageSequence.Where(pe => pe.FillUpMultiplier > 0).FirstOrDefault().PageReferenceKey];
                    for (int x = 0; x < pagesToAdd; x++)
                    {

                        p.Key += "_fillup_" + x;

                        pages.Insert(fillUpPageIndex, p);
                    }
                }
            }

            return pages;
        }

        public List<Page> CreatePagesFromTemplate(PageTemplate template)
        {
            List<Page> returnValue = SplitPages(template, 1);
            // Add static elements
            foreach (Page page in returnValue)
            {
                foreach (IPageElement element in template.StaticContents)
                {
                    page.Contents.Add(element);
                }
            }
            return returnValue;
        }

        public List<Page> SplitPages(PageTemplate pageTemplate, int pageNumber)
        {
            List<Page> pages = new List<Page>();
            SplitPageOutput output = SplitPages(pageTemplate.Contents.Elements.ToList(), pageTemplate.Layout);

            while (output.FittingElements.Count > 0)
            {
                Page page = Page.FromTemplate(pageTemplate,
                                              new ElementCollection(pageTemplate.Contents, output.FittingElements), pageNumber);
                pages.Add(page);
                pageNumber++;

                output = SplitPages(output.NonFittingElements, pageTemplate.Layout);
            }
            return pages;
        }

        public SplitPageOutput SplitPages(List<IPageElement> elements, LayoutObject container)
        {
            List<IPageElement> fittingElements = new List<IPageElement>();
            List<IPageElement> nonFittingElements = new List<IPageElement>();

            bool lastElementWasFitting = false;
           float moveDifference = 0f;

            Unit baseUnit = new Unit("20mm");

            foreach (IPageElement element in elements)
            {
                if (element.LayoutContainer.Layout.Bottom < container.Bottom)
                {
                    float verticalPosition = 0f;
                    if (lastElementWasFitting)
                    {
                        moveDifference =  container.Top.Points - element.Layout.Top.Points;
                        verticalPosition = container.Top.Points;
                        lastElementWasFitting = false;
                    }
                    else
                    {
                        verticalPosition = moveDifference + element.Layout.Top.Points;
                    }
                   
                    element.Layout.Top =
                        new Unit(verticalPosition);
                    if  (element is Rectangle)
                    {
                        ((Rectangle)element).Relocate();
                    }


                    // TODO: Move all vertical related elements to same position.


                    // element.LayoutContainer.MoveUp(element.Container.Page.Height);
                    nonFittingElements.Add(element);
                    
                }
                else
                {
                    lastElementWasFitting = true;
                    fittingElements.Add(element);
                }
            }

            return new SplitPageOutput(fittingElements, nonFittingElements);
        }

        public void RelocateElement(IElement element, float pageHeight)
        {
        }

        public class SplitPageOutput
        {
            public List<IPageElement> FittingElements { get; private set; }
            public List<IPageElement> NonFittingElements { get; private set; }

            public SplitPageOutput(List<IPageElement> fittingElements, List<IPageElement> nonFittingElements)
            {
                FittingElements = fittingElements;
                NonFittingElements = nonFittingElements;
            }
        }
    }
}