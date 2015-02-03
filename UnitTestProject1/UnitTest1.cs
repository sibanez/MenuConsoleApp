using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
   
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1Faild()
        {

            Assert.IsTrue(0==1,"ok");
        }

        [TestMethod]
        [ExpectedExceptionAttribute(typeof(AssertFailedException))]
        public void TestExpectedExceptionAttribute()
        {

          
        }

        [TestMethod]
        public void TestMethodOk()
        {
            Assert.IsTrue(true, "ok");
        }
        [TestMethod]
        [ExpectedExceptionAttribute(typeof(AssertFailedException))]
        public void TestExpectedExceptionAttributeOk()
        {

            Assert.IsTrue(0 == 1, "ok");
        }

    }
}
