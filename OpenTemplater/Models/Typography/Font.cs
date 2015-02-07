using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenTemplater.Common.Exceptions;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models.Typography.Collections;

namespace OpenTemplater.Models.Typography
{
    public class Font : BaseModel
    {
        private Collections.FontStyleCollection _fontStyles =
            new FontStyleCollection();
        
        private string _baseUri;
        private string _encoding;
        private bool _isEmbedded = false;
        private Unit _defaultFontSize;

        public Collections.FontStyleCollection Styles
        {
            get { return _fontStyles; }
        }

        public bool IsEmbedded
        {
            get { return _isEmbedded; }
        }

        public string Encoding
        {
            get { return _encoding; }
        }

        public Unit DefaultFontSize
        {
            get { return _defaultFontSize; }
        }

        public string BaseUri
        {
            get { return _baseUri; }
        }

        /// <summary>
        /// Creates an new font instance.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="baseUri"></param>
        /// <param name="encoding"></param>
        /// <param name="isEmbedded"></param>
        /// <param name="defaultFontSize"></param>
        public Font(string key, string baseUri, string encoding, string isEmbedded, string defaultFontSize) : base(key)
        {
            _baseUri = baseUri;
            _encoding = encoding;
            bool.TryParse(isEmbedded, out _isEmbedded);
            _defaultFontSize = new Unit(defaultFontSize);
        }

        ///<summary>
        ///</summary>
        ///<param name="fontStyle"></param>
        public void AddStyle(FontStyle fontStyle)
        {
            Styles.Add(fontStyle);
        }


    }
}