using System;
using System.Text.RegularExpressions;

namespace OpenTemplater.Models.Text
{
    /// <summary>
    /// Formats a textelement.
    /// </summary>
    public class Formatter
    {
        private readonly Regex _inputFormRegex;
        private readonly Regex _outputFormatRegex;

        public Formatter(string inputform, string outputFormat)
        {
            _inputFormRegex = new Regex(inputform);
            _outputFormatRegex = new Regex(outputFormat);
        }

        public string Format(string input)
        {
            if (_inputFormRegex.IsMatch(input))
            {
               
            }
            throw new NotImplementedException();
        }
    }
}