using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MenuConsoleApp.Controlers;
using MenuConsoleApp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MenuConsoleApp.UnitTest
{
    [TestClass]
    [ExcludeRunTestMenuConsoleApp]
    [ExcludeFromCodeCoverage]
    public class ControlersFactoryUnitTest
    {
        [TestMethod]
        public void ControlersFactoryCreateControlersTest()
        {
            var result =  ControlersFactory.CreateControlers();
            Assert.AreEqual(1,result.Count());
            
        }
    }
}