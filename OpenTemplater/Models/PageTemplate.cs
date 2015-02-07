using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Data.Xml;
using OpenTemplater.Models.Layout;

namespace OpenTemplater.Models
{
    public class PageTemplate : BasePage
    {
        private Content _staticContent;
        private Content _dynamicContent;

        public Content StaticContents
        {
            get { return _staticContent; }
            set { _staticContent = value; }
        }
        
        /// <summary>
        /// Dynamic contents of this template.
        /// </summary>
        public Content DynamicContents
        {
            get { return this.Contents; }
            set { this.Contents = value; }
        }

        public PageTemplate(Document document, String key, String width, String height, String colorType,
                            String bleeding, String layoutTop, string layoutLeft, string layoutWidth, string layoutHeight)
            : base(document, key, width, height, colorType, bleeding)
        {
            LayoutObject layout = new LayoutObject();
            layout.Width = new Unit(layoutWidth);
            layout.Height = new Unit(layoutHeight);
            layout.Top = Height - new Unit(layoutTop);
            layout.Left = new Unit(layoutLeft);
            _staticContent = new Content(this);

            SetLayout(layout);
        }

        public static PageTemplate Parse(Document document, XmlPageTemplateDefinition xmlDefinition)
        {
            return new PageTemplate(document, xmlDefinition.Key, xmlDefinition.Width, xmlDefinition.Height,
                                    xmlDefinition.Colortype,
                                    xmlDefinition.Bleeding, xmlDefinition.XmlLayout.VOffset.Value, xmlDefinition.XmlLayout.HOffset.Value, xmlDefinition.XmlLayout.Width.Value, xmlDefinition.XmlLayout.Height.Value);
        }
    }
}