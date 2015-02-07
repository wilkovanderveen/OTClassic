using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenTemplater.Models;
using OpenTemplater.Models.Collections;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Text;
using OpenTemplater.Models.Typography;
using OpenTemplater.Common.Measuring;
using OpenTemplater.Core.Modules;
using Rhino.Mocks;

namespace OpenTemplaterTest
{
    [TestFixture]
    public class TextModuleTest
    {
        [Test]
        public void ConvertToTextlines_Text()
        {
            Page page = new Page(new Document(), "PAGE", "200mm", "200mm", "rgb", "3mm", 1);

            Font f = new Font("FONT", @"C:\Windows\Fonts", "UTF-8", "false", "10pt");
            FontStyle fs = new FontStyle(f, "STYLE", "arial.ttf");
            f.AddStyle(fs);

            object[] paramList;

            Content container = new Content(page);

            paramList = new object[2];
            paramList[0] = container;
            paramList[1] = "TEXT";

            OpenTemplater.Models.Text.Text text = MockRepository.GeneratePartialMock<OpenTemplater.Models.Text.Text>(paramList);

            Paragraph p = new Paragraph(text, "10pt", "left");

            TextElement te = new TextElement(p, "Dit is een hele mooie tekst.", fs, new Unit(10), new Color("blue"));
            
            p.Add(te);
            p.Add(te);
            p.Add(te);
            
            text.Add(p);

            TextModule tm = new TextModule();
            TextlineCollection textlineCollection = tm.ConvertToTextlines(new Unit("35mm"), text);

            // There should be 4 texlines.
            Assert.AreEqual(4, textlineCollection.Count);

            // Each textline should be shorter than 35 milimeters.
            foreach(Textline textline in textlineCollection)
            {
                Assert.AreEqual(true, textline.Width.Points <= new Unit("35mm").Points);
            }

            // The total height of the textlines should be 40mm.
            Assert.AreEqual(40, textlineCollection.Height.Points);
            
        }

        [Test]
        public void TextOverflow()
        {
            Page page = new Page(new Document(), "PAGE", "200mm", "200mm", "rgb", "3mm", 1);

            Font f = new Font("FONT", @"C:\Windows\Fonts", "UTF-8", "false", "10pt");
            FontStyle fs = new FontStyle(f, "STYLE", "arial.ttf");
            f.AddStyle(fs);

            object[] paramList;

            Content container = new Content(page);

            paramList = new object[2];
            paramList[0] = container;
            paramList[1] = "TEXT";

            OpenTemplater.Models.Text.Text textNonFitting = MockRepository.GeneratePartialMock<OpenTemplater.Models.Text.Text>(paramList);
            OpenTemplater.Models.Text.Text textFitting = MockRepository.GeneratePartialMock<OpenTemplater.Models.Text.Text>(paramList);

            //Paragraph p = new Paragraph(text, "10pt", "left");

            //TextElement te = new TextElement(p, "Dit is een hele mooie tekst.", fs, new Unit(10), new Color("blue"));
        }

        [Test]
        public void Replace_PageNumber_10()
        {
            TextModule textModule = new TextModule();
            string result = textModule.Replace_PageNumber("Page [PageNumber]", 10);
            Assert.AreEqual("Page 10", result);

        }
    }
}
