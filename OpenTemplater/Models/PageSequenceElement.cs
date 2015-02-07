using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Data.Xml;

namespace OpenTemplater.Models
{
    public class PageSequenceElement
    {
        private string _pageReferenceKey;
        private string _pageTemplateReferenceKey;
        private int _fillUpMultiplier;
        public List<TemplateAction> _templateActions;

        /// <summary>
        /// 
        /// </summary>
        public String PageReferenceKey
        {
            get { return _pageReferenceKey; }
        }

        public String PageTemplateReferenceKey
        {
            get { return _pageTemplateReferenceKey; }
        }

        public List<TemplateAction> TemplateActions
        {
            get { return _templateActions; }
        }

        public int FillUpMultiplier
        {
            get { return _fillUpMultiplier; }
        }

        public PageSequenceElement(XmlPageSequenceElement element)
        {
            _templateActions = new List<TemplateAction>();
            foreach (XmlTemplateAction xmlTemplateAction in element.TemplateActions)
            {
                _templateActions.Add(TemplateAction.Parse(xmlTemplateAction));
            }

            if (element.FillUpMultiplier != null)
                _fillUpMultiplier = int.Parse(element.FillUpMultiplier); 

            _pageReferenceKey = element.StaticReference;
            _pageTemplateReferenceKey = element.TemplateReference;
        }

        public static PageSequenceElement Parse(XmlPageSequenceElement element)
        {
            PageSequenceElement returnValue = new PageSequenceElement(element);
            return returnValue;
        }
    }
}