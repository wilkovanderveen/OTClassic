using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Data.Xml.Elements
{
    public class TextElement
    {
        public string Value;
        public string Font;
        public string Style;
        public string Fontsize;
        public string Color;
        public string Charspacing;

        public TextElement(System.Xml.XmlNode textElementNode)
        {
            #region required attributes

            Value = textElementNode.InnerText;
            Font = textElementNode.Attributes["font"].Value;
            Style = textElementNode.Attributes["style"].Value;
            Fontsize = textElementNode.Attributes["fontsize"].Value;
            Color = textElementNode.Attributes["color"].Value;

            #endregion

            #region optional attributes

            if (textElementNode.Attributes["charspacing"] != null)
            {
                Charspacing = textElementNode.Attributes["charspacing"].Value;
            }

            #endregion
        }
    }
}