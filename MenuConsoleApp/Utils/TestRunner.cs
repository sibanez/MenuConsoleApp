using System;
using System.Linq;
using System.Reflection;
using MenuConsoleApp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MenuConsoleApp.Utils
{
    public class TestRunner
    {
        private int _totalNumberOfTests;
        private int _passedTests;

        public void Run(Assembly assembly)
        {

            var testClasses = assembly.GetTypes().Where(t => t.GetCustomAttributes(false).All(a => a is TestClassAttribute ));//&& a.GetType() != typeof(ExcludeRunTestMenuConsoleAppAttribute)));

            foreach (var testClass in testClasses)
            {
                Console.WriteLine("Test class {0} ", testClass.Name);
               
                var testMethods = testClass.GetMethods().Where(m => m.GetCustomAttributes(false).Any(a => a is TestMethodAttribute  ));
                var classInstance = Activator.CreateInstance(testClass);
                foreach (var testMethod in testMethods)
                {
                    if (testMethod.GetCustomAttributes(false).Any(a => a is ExpectedExceptionAttribute))
                    {

                        Invoka(testMethod, classInstance, true);
                      
                    }
                    else
                    {
                        var resultException = Invoka(testMethod, classInstance, false);

                        if (resultException == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\t Test {0} : Passed ", testMethod.Name);
                          
                        }
                    }

                    _totalNumberOfTests++;
                    
                }
            }

            Console.ResetColor();
            Console.WriteLine("Total test {0}, passedTests {1} Failed {2}", _totalNumberOfTests, _passedTests, _totalNumberOfTests-_passedTests);
        }
    
        private  Exception Invoka(MethodInfo method, Object target, bool expectedException)
        {
            try
            {
                method.Invoke(target, null);
                if (expectedException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t Test {0} ExpectedException: Faild", method.Name);
                    return null;
                }
                _passedTests++;

            }
            catch (TargetInvocationException te)
            {
                if (expectedException)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t Test {0} ExpectedException: Passed", method.Name);
                    _passedTests++;
                    return null;
                }
                if (te.InnerException == null)
                    throw;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t Test {0} : Faild", method.Name);
                return te;
               
            }
            return null;
        }
    }

   
}