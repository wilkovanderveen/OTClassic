using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Models;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;
using OpenTemplater.Models.Typography;

namespace OpenTemplaterTest
{
    [TestFixture]
    public class LayoutTest
    {
        [Test]
        public void RelateTopToBottom_5mm()
        {
            Color black = new Color("black");
            Document document = new Document();
            document.Colors.Add(black);
            Page page = new Page(document, "PAGE", "210mm" , "297mm", "rgb", "3mm", 1);
            Content content = new Content(page);

            Rectangle topRectangle = new Rectangle(content, "TOP", "black", "1mm");
            page.Add(topRectangle);

            Rectangle bottomRectangle = new Rectangle(content, "BOTTOM", "black", "1mm");
            page.Add(bottomRectangle);

            Unit topUnit = new Unit("10mm", new Relation(topRectangle, "TOP", "bottom"));
            Unit leftUnit = new Unit("10mm");
            Unit widthUnit = new Unit("20mm");
            Unit heightUnit = new Unit("30mm");

            LayoutContainer layoutContainerTop = new LayoutContainer(topRectangle, widthUnit, heightUnit, leftUnit,
                                                                  new Unit("10mm"));
            topRectangle.LayoutContainer = layoutContainerTop;
           

            LayoutContainer layoutContainerBottom = new LayoutContainer(bottomRectangle, widthUnit, heightUnit, leftUnit,
                                                                  topUnit);

            bottomRectangle.LayoutContainer = layoutContainerBottom;

            Assert.AreEqual(287, topRectangle.Layout.Top.Milimeters, 0.001d);
            Assert.AreEqual(257, topRectangle.Layout.Bottom.Milimeters, 0.001d);
            Assert.AreEqual(217, bottomRectangle.Layout.Bottom.Milimeters, 0.001d);
        }
    }
}
