using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Drawing;
using iTextSharp.text;
using OpenTemplater.Models;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using Paragraph=OpenTemplater.Models.Text.Paragraph;
using Rectangle = System.Drawing.Rectangle;

namespace OpenTemplater.Presentation.Bitmap
{
    public class Page : Interfaces.IContainer
    {
        private int _width;
        private int _height;
        private int _bleedWidth;
        private int _slugWidth;

        private System.Drawing.Bitmap _bitmap;

        public Page(Models.Page businessPage)
        {
            _width = Convert.ToInt32(businessPage.Width.Points);
            _height = Convert.ToInt32(businessPage.Height.Points);

            //TODO: Bleed and slug

            _bitmap = new System.Drawing.Bitmap(_width, _height);
            System.Drawing.Graphics pageGraphics = Graphics.FromImage(_bitmap);
            _bitmap.MakeTransparent();


            foreach (IPageElement bElement in businessPage.Contents.Elements)
            {
                switch (bElement.GetType().Name.ToLower())
                {
                    case "image":
                        Models.Image bImage = (Models.Image) bElement;
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

                        Models.Line bLine = (Models.Line) bElement;
                        Pen linePen =
                            new Pen(Color.FromArgb(bLine.Color.RGBColor.Red, bLine.Color.RGBColor.Green,
                                                   bLine.Color.RGBColor.Blue), bLine.Width.Points);
                        pageGraphics.DrawLine(linePen, bLine.LayoutContainer.Layout.Left.GetIntegerValue(),
                                              bLine.LayoutContainer.Layout.Top.GetIntegerValue(),
                                              bLine.LayoutContainer.Layout.Right.GetIntegerValue(),
                                              bLine.LayoutContainer.Layout.Bottom.GetIntegerValue());

                        break;

                    case "rectangle":

                        Models.Rectangle bRectangle = (Models.Rectangle) bElement;
                        Pen rectPen =
                            new Pen(Color.FromArgb(bRectangle.BorderColor.RGBColor.Red,
                                                   bRectangle.BorderColor.RGBColor.Green,
                                                   bRectangle.BorderColor.RGBColor.Blue,
                                                   bRectangle.BorderWidth.GetIntegerValue()));
                        System.Drawing.Rectangle rectangle =
                            new Rectangle(bElement.LayoutContainer.Layout.Left.GetIntegerValue(),
                                          bElement.LayoutContainer.Layout.Top.GetIntegerValue(),
                                          bElement.LayoutContainer.Layout.Width.GetIntegerValue(),
                                          bElement.LayoutContainer.Layout.Height.GetIntegerValue());
                        pageGraphics.DrawRectangle(rectPen, rectangle);

                        if (bRectangle.FillColor != null)
                        {
                            System.Drawing.Color fillColor = Color.FromArgb(bRectangle.FillColor.RGBColor.Red,
                                                                            bRectangle.FillColor.RGBColor.Green,
                                                                            bRectangle.FillColor.RGBColor.Blue);
                            pageGraphics.FillRectangle(new SolidBrush(fillColor), rectangle);
                        }

                        break;

                    case "text":
                        Models.Text.Text bText = (Models.Text.Text) bElement;

                        RectangleF placeholderRectangle =
                            new RectangleF(bText.LayoutContainer.Layout.Left.GetIntegerValue(),
                                           bText.LayoutContainer.Layout.Top.GetIntegerValue(),
                                           bText.LayoutContainer.Layout.Width.GetIntegerValue(),
                                           bText.LayoutContainer.Layout.Height.GetIntegerValue());

                        foreach (Models.Text.Paragraph bTextParagraph in bText.Paragraphs)
                        {
                            StringFormat paragraphFormat = new StringFormat();
                            paragraphFormat.LineAlignment = GetAligment(bTextParagraph.Alignment);


                            foreach (Models.Text.TextElement bTextElement in bTextParagraph.TextElements)
                            {
                                //TODO: Use logic from PDF text to calculate text width and height + placement.
                                System.Drawing.Font font = new Font(bTextElement.FontStyle.Path,
                                                                    bTextElement.FontSize.Points, FontStyle.Regular);

                                SolidBrush brush =
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

        private System.Drawing.StringAlignment GetAligment(Paragraph.AlignmentType alignmentType)
        {
            if (alignmentType == Paragraph.AlignmentType.left)
            {
                return System.Drawing.StringAlignment.Near;
            }
            if (alignmentType == Paragraph.AlignmentType.right)
            {
                return System.Drawing.StringAlignment.Far;
            }
            if (alignmentType == Paragraph.AlignmentType.center)
            {
                return System.Drawing.StringAlignment.Center;
            }
            return System.Drawing.StringAlignment.Near;
        }
    }
}