using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Typography
{
    public class Color : BaseModel
    {
        public CMYKColor CMYKColor;
        public RGBColor RGBColor;
        public PMSColor PMSColor;

        public Interfaces.IColorType this[string colortype]
        {
            get
            {
                string select = colortype.ToLower();
                switch (select)
                {
                    case "cmyk":
                        return CMYKColor;
                    case "rgb":
                        return RGBColor;
                    case "pms":
                        return PMSColor;
                }
                return null;
            }
        }

        public bool HasPMSColor
        {
            get
            {
                if (PMSColor != null)
                {
                    return true;
                }
                return false;
            }
        }

        public Color(string key) : base(key)
        {
           
        }

        public override string ToString()
        {
            StringBuilder colorString = new StringBuilder();
            colorString.Append(this.CMYKColor.ToString());
            return colorString.ToString();
        }
    }
}