using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Core.Modules;

namespace OpenTemplater.Models.Layout
{
    public class LayoutContainer
    {
        private LayoutModule _layoutModule = new LayoutModule();

        #region private members

        private LayoutObject _virtualLayout = new LayoutObject();
        private LayoutObject _layout = new LayoutObject();

        public LayoutObject Layout
        {
            get { return _layout; }
        }

        private IPageElement _element;

        #endregion

        #region public properties

        public void SetLayout()
        {
            _layout.Width = _layoutModule.GetAbsoluteWidth(_virtualLayout.Width);
            _layout.Height = _layoutModule.GetAbsoluteHeight(_virtualLayout.Height);
            _layout.Left = _layoutModule.GetAbsoluteLeft(_virtualLayout.Left);
            _layout.Top = _layoutModule.GetAbsoluteTop(this, _virtualLayout.Top);
        }

        public void MoveUp(Unit value)
        {
            Layout.Top = new Unit(Layout.Top.Points + value.Points);
        }

        /// <summary>
        /// IElement object to which this layout belongs. 
        /// </summary>
        public IElement Element
        {
            get { return _element; }
        }

        #endregion

        /// <summary>
        /// Creates a layout for an element based on absolute values.
        /// </summary>
        /// <param name="width">string representing the width of the element.</param>
        /// <param name="height">string representing the height of the element.</param>
        /// <param name="xPosition">string representing the horizontal position of the element.</param>
        /// <param name="yPosition">string representing the vertical position of the element.</param>
        public LayoutContainer(IPageElement parentElement, string width, string height, string xPosition,
                               string yPosition)
        {
            _element = parentElement;
            _virtualLayout.Width = new Unit(width);
            _virtualLayout.Height = new Unit(height);
            _virtualLayout.Left = new Unit(xPosition);
            _virtualLayout.Top = new Unit(yPosition);

            SetLayout();
        }

        /// <summary>
        /// Creates a layout for an element based on absolute values.
        /// </summary>
        /// <param name="width">Unit representing the width of the element.</param>
        /// <param name="height">Unit representing the height of the element.</param>
        /// <param name="xPosition">Unit representing the horizontal position of the element.</param>
        /// <param name="yPosition">Unit representing the vertical position of the element.</param>
        public LayoutContainer(IPageElement parentElement, Unit width, Unit height, Unit xPosition,
                              Unit yPosition)
        {
            _element = parentElement;
            _virtualLayout.Width =width;
            _virtualLayout.Height = height;
            _virtualLayout.Left = xPosition;
            _virtualLayout.Top = yPosition;

            SetLayout();
        }

        /// <summary>
        /// Constructs a layout object from a XML data object.
        /// </summary>
        /// <param name="parentElement">The element which relates to this layout.</param>
        /// <param name="dataXmlLayoutDefinition">The data object which is the base for this layout object.</param>
        public LayoutContainer(IPageElement parentElement, Data.Xml.Layout.XmlLayoutDefinition dataXmlLayoutDefinition)
        {
            _element = parentElement;
            _virtualLayout.Width = new Unit(dataXmlLayoutDefinition.Width.Value);
            _virtualLayout.Width.AddRelation(GetRelation(dataXmlLayoutDefinition.Width));
            _virtualLayout.Height = new Unit(dataXmlLayoutDefinition.Height.Value);
            _virtualLayout.Height.AddRelation(GetRelation(dataXmlLayoutDefinition.Height));
            _virtualLayout.Left = new Unit(dataXmlLayoutDefinition.HOffset.Value);
            _virtualLayout.Left.AddRelation(GetRelation(dataXmlLayoutDefinition.HOffset));
            _virtualLayout.Top = new Unit(dataXmlLayoutDefinition.VOffset.Value);
            _virtualLayout.Top.AddRelation(GetRelation(dataXmlLayoutDefinition.VOffset));

            ResizeOptions resizeOptions = GetResizeOptions(dataXmlLayoutDefinition.Width);
            if (resizeOptions != null)
            {
                _virtualLayout.Width.AddResizeOptions(resizeOptions);
            }

            resizeOptions = GetResizeOptions(dataXmlLayoutDefinition.Height);
            if (resizeOptions != null)
            {
                _virtualLayout.Height.AddResizeOptions(resizeOptions);
            }

            SetLayout();
        }

        /// <summary>
        /// Construct a relation object from a data dimension object.
        /// </summary>
        /// <param name="dataDimension"></param>
        private Relation GetRelation(Data.Xml.Layout.Dimension dataDimension)
        {
            if (dataDimension.Relation != null)
            {
                Relation returnRelation = new Relation(_element, dataDimension.Relation.Element,
                                                       dataDimension.Relation.From);
                if (!_element.RelatedElements.ContainsKey(this.Element.Key))
                {
                    _element.RelatedElements.Add(this.Element.Key, this.Element);
                }
                return returnRelation;
            }
            return null;
        }


        private ResizeOptions GetResizeOptions(Data.Xml.Layout.Dimension dataDimension)
        {
            if (dataDimension.Resizing != null)
            {
                ResizeOptions returnOptions =
                    new ResizeOptions((ResizeType) Enum.Parse(typeof (ResizeType), dataDimension.Resizing.Type, true));

                if (!String.IsNullOrEmpty(dataDimension.Resizing.Maximum))
                    returnOptions.Maximum = new Unit(dataDimension.Resizing.Maximum);

                if (!String.IsNullOrEmpty(dataDimension.Resizing.Minimum))
                    returnOptions.Minimum = new Unit(dataDimension.Resizing.Minimum);

                return returnOptions;
            }
            return null;
        }

        public void TryResize(Unit newWidth, Unit newHeight)
        {
            if (_virtualLayout.Width.HasResizeOptions)
            {
                Layout.Width = GetNewUnit(Layout.Width, newWidth, Layout.Width.ResizeOptions.Minimum,
                                          Layout.Width.ResizeOptions.Maximum,
                                          Layout.Width.ResizeOptions.ResizeType);
            }

            if (_virtualLayout.Height.HasResizeOptions)
            {
                Layout.Height = GetNewUnit(Layout.Height, newHeight, Layout.Height.ResizeOptions.Minimum,
                                           Layout.Height.ResizeOptions.Maximum,
                                           Layout.Height.ResizeOptions.ResizeType);
            }
        }

        private Unit GetNewUnit(Unit original, Unit calculated, Unit minimum, Unit maximum, ResizeType resizeType)
        {
            return
                new Unit(_layoutModule.GetNewUnit(original.Points, calculated.Points,
                                                  minimum != null ? (float?) minimum.Points : null,
                                                  maximum != null ? (float?) maximum.Points : null, resizeType));
        }
    }
}