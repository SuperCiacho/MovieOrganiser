
using NUnit.Framework;
using MovieOrganiser.Utils;

namespace MovieOrganiser.Tests
{
    [TestFixture]
    public class CatalogToolTests
    {
        private CatalogTool catalogTool;

        [SetUp]
        public void Setup()
        {
            this.catalogTool = CatalogTool.Instance;
        }

        [Test]
        public void TestMethod1()
        {
        }
    }
}
