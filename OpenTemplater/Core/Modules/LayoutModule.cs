using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;

namespace OpenTemplater.Core.Modules
{
    public class LayoutModule
    {
        /// <summary>
        /// Calculates an new unit based on the original dimension value, the calculated content value, the minimum value, the maximum value and its resizetype.
        /// </summary>
        /// <param name="original">Original dimension value.</param>
        /// <param name="calculated">Calculated content value.</param>
        /// <param name="minimum">Minimum dimension value.</param>
        /// <param name="maximum">Maximum dimension value.</param>
        /// <param name="resizeType">Resizetype which should be used to resize the dimension.</param>
        /// <returns></returns>
        public float GetNewUnit(float original, float calculated, float? minimum, float? maximum, ResizeType resizeType)
        {
            float returnValue = original;

            if (resizeType == ResizeType.GrowAndShrink)
            {
                if (calculated >= original)
                {
                    if (maximum.HasValue)
                    {
                        returnValue = Math.Min(calculated, maximum.Value);
                    }
                    else
                    {
                        returnValue = calculated;
                    }
                }
                else
                {
                    if (minimum.HasValue)
                    {
                        returnValue = Math.Max(calculated, minimum.Value);
                    }
                    else
                    {
                        returnValue = calculated;
                    }
                }
            }

            if (ResizeType.Grow == resizeType)
            {
                if (calculated > original)
                {
                    if (maximum.HasValue)
                    {
                        returnValue = Math.Min(calculated, maximum.Value);
                    }
                    else
                    {
                        returnValue = calculated;
                    }
                }
            }

            if (ResizeType.Shrink == resizeType)
            {
                if (calculated < original)
                {
                    if (minimum.HasValue)
                    {
                        returnValue = Math.Max(calculated, minimum.Value);
                    }
                    else
                    {
                        returnValue = calculated;
                    }
                }
            }

            return returnValue;
        }

        public Unit GetAbsoluteWidth(Unit givenWidth)
        {
            float floatValue = givenWidth.Points;
            if (givenWidth.HasRelation)
            {
                if (givenWidth.Relation.From == "width")
                {
                    floatValue = givenWidth.Relation.Element.Layout.Width.Points + givenWidth.Points;
                }

                if (givenWidth.Relation.From == "height")
                {
                    floatValue = givenWidth.Relation.Element.Layout.Height.Points + givenWidth.Points;
                }
            }

            return new Unit(floatValue, givenWidth.Relation, givenWidth.ResizeOptions);
        }

        public Unit GetAbsoluteHeight(Unit virtualHeight)
        {
            float floatValue = virtualHeight.Points;
            if (virtualHeight.HasRelation)
            {
                // Get height of other element
                switch (virtualHeight.Relation.From)
                {
                    case "height":
                        floatValue = (virtualHeight.Relation.Element.Layout.Height.Points + virtualHeight.Points);
                        break;
                    case "width":
                        floatValue = virtualHeight.Relation.Element.Layout.Width.Points + virtualHeight.Points;
                        break;
                    default:
                        throw new Exception("Invalid from argument, should be height or width.");
                }
            }

            return new Unit(floatValue, virtualHeight.Relation, virtualHeight.ResizeOptions);
        }

        public Unit GetAbsoluteLeft(Unit virtualLeft)
        {
            float floatValue = virtualLeft.Points;
            if (virtualLeft.HasRelation)
            {
                // Get height of other element
                switch (virtualLeft.Relation.From)
                {
                    case "left":
                        floatValue = (virtualLeft.Relation.Element.Layout.Left.Points + virtualLeft.Points);
                        break;
                    case "right":
                        floatValue = virtualLeft.Relation.Element.Layout.Right.Points + virtualLeft.Points;
                        break;
                    default:
                        throw new Exception("Invalid from argument, should be height or width.");
                }
            }
            return new Unit(floatValue);
        }

        public Unit GetAbsoluteTop(LayoutContainer layout, Unit virtualTop)
        {
            // Set value to reverted Y-Axis
            float floatValue = layout.Element.Page.Height.Points - virtualTop.Points;

            if (virtualTop.HasRelation)
            {
                if (virtualTop.Relation.From == "top")
                {
                    floatValue = virtualTop.Relation.Element.Layout.Top.Points - virtualTop.Points;
                }

                if (virtualTop.Relation.From == "bottom")
                {
                    floatValue = virtualTop.Relation.Element.Layout.Bottom.Points - virtualTop.Points;
                }
            }
            return new Unit(floatValue);
        }

        public void RecalculateContent(Rectangle rectangle)
        {
        }
    }
}