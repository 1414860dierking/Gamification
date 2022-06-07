using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.Fail();
        }

        public void TestMethod2()
        {
            Assert.IsNotNull(1);
        }
    }
}