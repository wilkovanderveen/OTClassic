using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenTemplater.Models;
using OpenTemplater.Core.Modules;

namespace OpenTemplaterTest
{
    [TestFixture]
    public class LayoutModuleTest
    {
        LayoutModule _layoutModule = new LayoutModule();

        [Test]
        public void GetNewUnit_ResizeNone_ReturnOriginal_20()
        {
            float result = _layoutModule.GetNewUnit(20, 40, 20, 60, ResizeType.None);

            Assert.AreEqual(20f, result);
        }

        [Test]
        public void GetNewUnit_ResizeGrowShrink_ReturnMaximum_30()
        {
            float result = _layoutModule.GetNewUnit(20, 40, 20, 30, ResizeType.GrowAndShrink);

            Assert.AreEqual(30f, result);
        }

        [Test]
        public void GetNewUnit_ResizeGrowShrink_ReturnMinimum_20()
        {
            float result = _layoutModule.GetNewUnit(20, 10, 20, 30, ResizeType.GrowAndShrink);

            Assert.AreEqual(20f, result);
        }

        [Test]
        public void GetNewUnit_ResizeGrowShrink_ReturnCalculated_25()
        {
            float result = _layoutModule.GetNewUnit(20, 25, 20, 30, ResizeType.GrowAndShrink);

            Assert.AreEqual(25f, result);
        }

        [Test, Explicit]
        public void GetNewUnit_ResizeGrowShrink_ReturnOriginal_40()
        {
            float result = _layoutModule.GetNewUnit(40, 70, 80, 100, ResizeType.GrowAndShrink);
            Assert.AreEqual(40f, result);
        }
    }
}
