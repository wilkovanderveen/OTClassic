using System;
using System.Drawing;
using OpenTemplater.Models;
using OpenTemplater.Models.Text;
using OpenTemplater.Presentation.Bitmap.Interfaces;
using Image = OpenTemplater.Models.Image;
using Rectangle = System.Drawing.Rectangle;

namespace OpenTemplater.Output.Bitmap
{
    public class Page : IContainer
    {
        private System.Drawing.Bitmap _bitmap;
        private int _bleedWidth;
        private int _height;
        private int _slugWidth;
        private int _width;

        public Page(Models.Page businessPage)
        {
            _width = Convert.ToInt32(businessPage.Width.Points);
            _height = Convert.ToInt32(businessPage.Height.Points);

            //TODO: Bleed and slug

            _bitmap = new System.Drawing.Bitmap(_width, _height);
            Graphics pageGraphics = Graphics.FromImage(_bitmap);
            _bitmap.MakeTransparent();


            foreach (IPageElement bElement in businessPage.Contents.Elements)
            {
                switch (bElement.GetType().Name.ToLower())
                {
                    case "image":
                        var bImage = (Image) bElement;
                        try
                        {
                            System.Drawing.Image dImage = System.Drawing.Image.FromFile(new Uri(bImage.Uri).LocalPath);
                            pageGraphics.DrawImage(dImage,
                                new Rectangle(bImage.LayoutContainer.Layout.Left.GetIntegerValue(),
                                    bImage.LayoutContainer.Layout.Top.GetIntegerValue(),
                                    bImage.LayoutContainer.Layout.Width.GetIntegerValue(),
                                    bImage.LayoutContainer.Layout.Height.GetIntegerValue()));
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        break;
                    case "line":

                        var bLine = (Line) bElement;
                        var linePen =
                            new Pen(Color.FromArgb(bLine.Color.RGBColor.Red, bLine.Color.RGBColor.Green,
                                bLine.Color.RGBColor.Blue), bLine.Width.Points);
                        pageGraphics.DrawLine(linePen, bLine.LayoutContainer.Layout.Left.GetIntegerValue(),
                            bLine.LayoutContainer.Layout.Top.GetIntegerValue(),
                            bLine.LayoutContainer.Layout.Right.GetIntegerValue(),
                            bLine.LayoutContainer.Layout.Bottom.GetIntegerValue());

                        break;

                    case "rectangle":

                        var bRectangle = (Models.Rectangle) bElement;
                        var rectPen =
                            new Pen(Color.FromArgb(bRectangle.BorderColor.RGBColor.Red,
                                bRectangle.BorderColor.RGBColor.Green,
                                bRectangle.BorderColor.RGBColor.Blue,
                                bRectangle.BorderWidth.GetIntegerValue()));
                        var rectangle =
                            new Rectangle(bElement.LayoutContainer.Layout.Left.GetIntegerValue(),
                                bElement.LayoutContainer.Layout.Top.GetIntegerValue(),
                                bElement.LayoutContainer.Layout.Width.GetIntegerValue(),
                                bElement.LayoutContainer.Layout.Height.GetIntegerValue());
                        pageGraphics.DrawRectangle(rectPen, rectangle);

                        if (bRectangle.FillColor != null)
                        {
                            Color fillColor = Color.FromArgb(bRectangle.FillColor.RGBColor.Red,
                                bRectangle.FillColor.RGBColor.Green,
                                bRectangle.FillColor.RGBColor.Blue);
                            pageGraphics.FillRectangle(new SolidBrush(fillColor), rectangle);
                        }

                        break;

                    case "text":
                        var bText = (Text) bElement;

                        var placeholderRectangle =
                            new RectangleF(bText.LayoutContainer.Layout.Left.GetIntegerValue(),
                                bText.LayoutContainer.Layout.Top.GetIntegerValue(),
                                bText.LayoutContainer.Layout.Width.GetIntegerValue(),
                                bText.LayoutContainer.Layout.Height.GetIntegerValue());

                        foreach (Paragraph bTextParagraph in bText.Paragraphs)
                        {
                            var paragraphFormat = new StringFormat();
                            paragraphFormat.LineAlignment = GetAligment(bTextParagraph.Alignment);


                            foreach (TextElement bTextElement in bTextParagraph.TextElements)
                            {
                                //TODO: Use logic from PDF text to calculate text width and height + placement.
                                var font = new Font(bTextElement.FontStyle.Path,
                                    bTextElement.FontSize.Points, FontStyle.Regular);

                                var brush =
                                    new SolidBrush(Color.FromArgb(bTextElement.Color.RGBColor.Red,
                                        bTextElement.Color.RGBColor.Green,
                                        bTextElement.Color.RGBColor.Blue));
                                pageGraphics.DrawString(bTextElement.Text, font, brush, placeholderRectangle,
                                    paragraphFormat);
                                SizeF stringArea = pageGraphics.MeasureString(bTextElement.Text, font,
                                    bElement.LayoutContainer.Layout.Width.
                                        GetIntegerValue(), paragraphFormat);
                            }
                        }

                        break;
                }


                pageGraphics.Save();
            }

            _bitmap.Save("Page_" + businessPage.Key + ".png");
        }

        private StringAlignment GetAligment(Paragraph.AlignmentType alignmentType)
        {
            if (alignmentType == Paragraph.AlignmentType.left)
            {
                return StringAlignment.Near;
            }
            if (alignmentType == Paragraph.AlignmentType.right)
            {
                return StringAlignment.Far;
            }
            if (alignmentType == Paragraph.AlignmentType.center)
            {
                return StringAlignment.Center;
            }
            return StringAlignment.Near;
        }
    }
}