using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenTemplater.Common.Measuring;

namespace OpenTemplaterTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void CreateMilimeterUnit()
        {
            Unit unit = new Unit("15mm");

            Assert.AreEqual(15, unit.Milimeters);
            Assert.AreEqual((1.5f/2.54f), unit.Inches);
            Assert.AreEqual((72 / 25.4) * 15, unit.Points, 1e-4);
        }

        [Test]
        public void CreateInchUnit()
        {
            Unit unit = new Unit("15inch");

            Assert.AreEqual((15 * 25.4), unit.Milimeters);
            Assert.AreEqual((15), unit.Inches);
            Assert.AreEqual(72 * 15, unit.Points, 1e-4);
        }

        [Test]
        public void CreatePointUnit()
        {
            Unit unit = new Unit("15pt");

            Assert.AreEqual((15f / 72f) * 25.4, unit.Milimeters, 1e-4);
            Assert.AreEqual((15f / 72f), unit.Inches);
            Assert.AreEqual(15, unit.Points, 1e-4);
        }
    }
}
