using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Typography
{
    public class PMSColor : Interfaces.IColorType
    {
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public PMSColor(string name)
        {
            _name = name;
        }
    }
}