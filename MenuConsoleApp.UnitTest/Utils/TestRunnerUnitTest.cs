using System.Diagnostics.CodeAnalysis;
using MenuConsoleApp.Core;
using MenuConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MenuConsoleApp.UnitTest.Utils
{
    [TestClass]
    [ExcludeRunTestMenuConsoleApp]
    [ExcludeFromCodeCoverage]
    public class TestRunnerUnitTest
    {
        [TestMethod]
      
        public void TestRunnerRun()
        {

          
            var target = new TestRunner();
            target.Run(typeof(TestRunnerUnitTestMock).Assembly);

            
        }
    }

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestRunnerUnitTestMock
    {
        [TestMethod]
        public void TestMethod1Faild()
        {

            Assert.IsTrue(0 == 1, "ok");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestExpectedExceptionAttribute()
        {


        }

        [TestMethod]
        public void TestMethodOk()
        {
            Assert.IsTrue(true, "ok");
        }
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestExpectedExceptionAttributeOk()
        {

            Assert.IsTrue(0 == 1, "ok");
        }
    }
}
