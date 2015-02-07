using OpenTemplater.Common.Measuring;
using OpenTemplater.Core.Modules;
using OpenTemplater.Models.Collections;

namespace OpenTemplater.Models.Text
{
    /// <summary>
    /// The text class.
    /// </summary>
    public class Text : BaseElement, IPageElement
    {
        private readonly float _rotation;

        #region enumerations

        public enum AlignmentType
        {
            top,
            middle,
            bottom
        }

        #endregion

        #region private members

        private string _overflowElement;
        private float _textHeight;

        #endregion

        #region public properties

        public ParagraphCollection Paragraphs { get; private set; }

        public Unit ContentHeight
        {
            get { return new Unit(_textHeight); }
        }

        public bool IsOverflowed
        {
            get
            {
                bool returnValue = false;
                if (ContentHeight.Points > LayoutContainer.Layout.Height.Points)
                {
                    returnValue = true;
                }
                return returnValue;
            }
        }

        ///<summary>
        ///</summary>
        public IElement OverflowElement
        {
            get
            {
                if (_overflowElement != null)
                {
                    return Page.HasKey(_overflowElement) ? Page[_overflowElement] : null;
                }
                return null;
            }
        }

        public AlignmentType VerticalAlignment { get; set; }

        #endregion

        public Text(Content container, string key, float rotation) : base(key, container.Parent)
        {
            Paragraphs = new ParagraphCollection();
            VerticalAlignment = AlignmentType.top;
            _rotation = rotation;
            Key = key;
            Container = container;
        }

        public TextlineCollection Textlines { get; private set; }

        public void Add(Paragraph paragraph)
        {
            Paragraphs.Add(paragraph);
        }

        public void SetOverflowElement(string element)
        {
            _overflowElement = element;
        }

        public void SetTextlines()
        {
            var textModule = new TextModule();

            TextlineCollection lines = textModule.ConvertToTextlines(LayoutContainer.Layout.Width, this);
            TextlineMeasuringOutput output = textModule.SplitTextlines(lines,
                                                                       LayoutContainer.Layout.Height.Points);

            if (Textlines == null)
            {
                Textlines = output.FittingLines;

                LayoutContainer.TryResize(Textlines.Width, Textlines.Height);

                var overflowText = OverflowElement as Text;

                if (overflowText != null)
                {
                    overflowText.Textlines.AddRange(output.NotFittingLines);
                }
            }
        }
    }
}