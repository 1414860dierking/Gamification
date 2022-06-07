using NUnit.Framework;

namespace Gamification.UnitTests
{
    [TestFixture]
    public class UnitTestFirst
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            Assert.Fail();
        }
    }
}
