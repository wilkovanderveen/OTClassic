using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Layout;
using OpenTemplater.Models.Text;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Common.Measuring.Text;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;
using Paragraph = OpenTemplater.Models.Text.Paragraph;
using TextElement = OpenTemplater.Models.Text.TextElement;

namespace OpenTemplater.Core.Modules
{
    public class TextModule
    {
        /// <summary>
        /// Converts a paragraph to a textline list.
        /// </summary>
        /// <param name="maximumWidth">Unit to use for measuring the maximum width of an textline</param>
        /// <param name="paragraph">paragraph to convert to textlines.</param>
        /// <returns></returns>
        public TextlineCollection ConvertToTextlines(Unit maximumWidth, Paragraph paragraph)
        {
            IList<Textline> result = new List<Textline>();
            float maximumPointWidth = maximumWidth.Points;
            float totalMeasuredWidth = 0f;
            Textline currentTextline = new Textline(paragraph);

            TextElementCollection splittedTextElements = new TextElementCollection();

            if (paragraph.TextElements.Count() > 0)
            {
                foreach (TextElement textElement in paragraph.TextElements)
                {
                    splittedTextElements.AddRange(ConvertToTextElements(textElement));
                }

                foreach (TextElement splittedTextElement in splittedTextElements)
                {
                    float width = MeasureTextElementWidth(splittedTextElement);
                    float tempTotalWidth = totalMeasuredWidth + width;

                    if (tempTotalWidth > maximumPointWidth)
                    {
                        currentTextline.Width = new Unit(totalMeasuredWidth);
                        currentTextline.Height = paragraph.Leading;
                        result.Add(currentTextline);
                        currentTextline = new Textline(paragraph);
                        totalMeasuredWidth = width;
                    }
                    else
                    {
                        totalMeasuredWidth = tempTotalWidth;
                    }

                    currentTextline.Add(splittedTextElement);
                }

                currentTextline.Width = new Unit(totalMeasuredWidth);
            }
            else
            {
                currentTextline.Width = new Unit(0f);
            }

            currentTextline.Height = paragraph.Leading;
            currentTextline.EndsParagraph = true;

            result.Add(currentTextline);
            return new TextlineCollection(result.ToList());
        }

        /// <summary>
        /// Converts a text object to zero or more textlines.
        /// </summary>
        /// <param name="maximumWidth">Maximum width in units.</param>
        /// <param name="text"></param>
        /// <returns></returns>
        public TextlineCollection ConvertToTextlines(Unit maximumWidth, Text text)
        {
#warning: TODO: Implement splitting in whole paragraphs.
            TextlineCollection result = new TextlineCollection();
            foreach (Paragraph paragraph in text.Paragraphs)
            {
                result.AddRange(ConvertToTextlines(maximumWidth, paragraph));
            }
            return result;
        }

        /// <summary>
        /// Converts a textelement to a list with atomic textelements.
        /// </summary>
        /// <param name="textElement">Textelement to convert.</param>
        /// <returns>List with the resulting textelements.</returns>
        public TextElementCollection ConvertToTextElements(TextElement textElement)
        {
            TextElementCollection result = new TextElementCollection();

            SplitCharacterList splitCharacterList = new SplitCharacterList();
            splitCharacterList.Add(' ', SplitAction.Remove);
            splitCharacterList.Add('-', SplitAction.Add);
            splitCharacterList.Add('.', SplitAction.Add);
            splitCharacterList.Add(',', SplitAction.Add);
            TextElementCollection textElements = textElement.Split(splitCharacterList);

            return textElements;
        }

        public float MeasureTextElementWidth(TextElement textElement)
        {
            Chunk textChunk = new Chunk(textElement.Text);
            textChunk.Font = new FontStyleDefinition(textElement.FontStyle).Font;
            textChunk.Font.Size = textElement.FontSize.Points;

            return textChunk.GetWidthPoint();
        }

        public TextlineMeasuringOutput SplitTextlines(TextlineCollection textlineCollection, float maximumHeight)
        {
            TextlineCollection fittingLines = new TextlineCollection();
            TextlineCollection notFittingLines = new TextlineCollection();

            float measuredHeight = 0;

            foreach (Textline currentTextline in textlineCollection)
            {
                if (measuredHeight < maximumHeight)
                {
                    fittingLines.Add(currentTextline);
                    measuredHeight += currentTextline.Height.Points;
                }
                else
                {
                    notFittingLines.Add(currentTextline);
                }
            }
            return new TextlineMeasuringOutput(fittingLines, notFittingLines);
        }

        public TextElement ParseText(TextElement textElement)
        {
            return null;
            //textElement.Parent.Page 
        }


        public string Replace_PageNumber(string text, int pageNumber)
        {
            return Replace(text, "PageNumber", pageNumber.ToString());
        }

        private string Replace(string text, string systemVariable, string newText)
        {
            Regex tagRegex = new Regex(@"\[(" + systemVariable + @")\]");
            string result = tagRegex.Replace(text, newText);

            return result;
        }
    }

    public class TextlineMeasuringOutput
    {
        public TextlineCollection FittingLines { get; private set; }
        public TextlineCollection NotFittingLines { get; private set; }

        public TextlineMeasuringOutput(TextlineCollection fittingLines, TextlineCollection notFittinglines)
        {
            FittingLines = fittingLines;
            NotFittingLines = notFittinglines;
        }
    }
}