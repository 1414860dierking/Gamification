using NUnit.Framework;

namespace Gamification
{
    public class UnitTest
    {
        [Test]

        public void Test()
        {
            Assert.Pass();
        }

        [Test]

        public void Test2MustBeFailed()
        {
            Assert.Fail();
        }
    }
}
