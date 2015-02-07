using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater
{
    public class ConfigSettings
    {
        private string _templatePath;

        private string _fontPath;
        private bool _fontPathIsRelative;

        /// <summary>
        /// Gets the path to the directory containing the fonts to use.
        /// </summary>
        public string FontPath
        {
            get
            {
                if (_fontPath != null)
                {
                    if (_fontPathIsRelative)
                    {
                        return _templatePath + "//" + _fontPath;
                    }
                    else
                    {
                        return _fontPath;
                    }
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// Sets the path to the directory containing the fonts to use. The path can be absoulute or relative.
        /// </summary>
        /// <param name="path">Path to the collection of fonts.</param>
        /// <param name="isRelative">Is the path relative to the template.</param>
        public void SetFontPath(string path, bool isRelative)
        {
            _fontPath = path;
            _fontPathIsRelative = isRelative;
        }

        public void SetFontPath(string path)
        {
            SetFontPath(path, false);
        }
    }
}
