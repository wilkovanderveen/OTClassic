using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Data.Xml;

namespace OpenTemplater.Models
{
    public class TemplateAction
    {
        public TemplateAction(XmlTemplateAction xmlTemplateAction)
        {
        }

        public static TemplateAction Parse(XmlTemplateAction xmlTemplateAction)
        {
            return new TemplateAction(xmlTemplateAction);
        }
    }
}