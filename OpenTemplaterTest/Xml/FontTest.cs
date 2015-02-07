using System.Xml;
using NUnit.Framework;
using OpenTemplater.Data.Xml.Typography;
using Rhino.Mocks;

namespace OpenTemplaterTest.Xml
{
    [TestFixture]
    class FontTest
    {
        [Test]
        public void CreateFontTest()
        {
            MockRepository mockRepository = new MockRepository();

            object[] args = {null};

            XmlDocument fontNode = new XmlDocument();
            
            Font font = new Font(fontNode, true);

            
        }
   
    }
}
