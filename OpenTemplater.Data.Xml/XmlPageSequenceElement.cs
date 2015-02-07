using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenTemplater.Data.Xml
{
    public class XmlPageSequenceElement
    {
        public List<XmlTemplateAction> TemplateActions;
        public string StaticReference;
        public string TemplateReference;
        public string FillUpMultiplier;

        public XmlPageSequenceElement(XmlNode pageSequenceNode)
        {
            TemplateActions = new List<XmlTemplateAction>();
            foreach (XmlNode actionNode in pageSequenceNode.SelectNodes("*"))
            {
                TemplateActions.Add(new XmlTemplateAction(actionNode));
            }

            XmlAttribute fillUpMultiplier = pageSequenceNode.Attributes["fillUpMultiplier"];
            if (fillUpMultiplier != null)
            {
                FillUpMultiplier = fillUpMultiplier.Value;
            }

            if (pageSequenceNode.LocalName.Equals("staticPage"))
            {
                XmlAttribute staticReferenceNode = pageSequenceNode.Attributes["refKey"];
                if (staticReferenceNode != null)
                {
                    StaticReference = staticReferenceNode.Value;
                    return;
                }
            }

            if (pageSequenceNode.LocalName.Equals("dynamicPage"))
            {
                XmlAttribute templateReferenceNode = pageSequenceNode.Attributes["refKey"];
                if (templateReferenceNode != null)
                {
                    TemplateReference = templateReferenceNode.Value;
                    return;
                }
            }
        }
    }
}