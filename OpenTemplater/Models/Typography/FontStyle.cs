using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models.Typography
{
    public class FontStyle : BaseModel 
    {
        private string _filename;
        private Font _font;

        public string CollectionKey
        {
            get { return _font.Key + "_" + Key; }
        }

        /// <summary>
        /// The full path the file containing the font definition.
        /// </summary>
        public string Path
        {
            get { return Font.BaseUri + "/" + _filename; }
        }

        public Font Font
        {
            get { return _font; }
        }

        public FontStyle(Font font, string key, string filename) : base(key)
        {
            _filename = filename;
            _font = font;
        }
    }
}