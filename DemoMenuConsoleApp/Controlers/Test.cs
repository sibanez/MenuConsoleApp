using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MenuConsoleApp.Controlers;
using MenuConsoleApp.Controls.ListBox;
using MenuConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoMenuConsoleApp.Controlers
{
    [System.ComponentModel.Description("Ejecutar Test")]
    public class Test : IConsoleControler
    {
        public int Order
        {
            get { return 1; }

        }




        public void Run()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (path == null) return;
            var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFile).ToList();

            IList<Assembly> assembliesTest = (from assembly in assemblies
                let testClasses = assembly.GetTypes()
                    .Where(t => t.GetCustomAttributes(false).Any(a => a is TestClassAttribute))
                where testClasses.Any()
                select assembly).ToList();

            var menu = assembliesTest.Select(a => a.GetName().Name).ToList();

            Star(menu);
        }

        public void Star(IList<string> menuNames)
        {
            var menu = new ConsoleListBox();
            menu.Changed += (o, eventArgs) =>
            {
                menu.CleanUp();
                var testRunner = new TestRunner();
                switch (o)
                {
                    case "Salir":
                        return;
                    case "Ejecutar Todos":
                        foreach (var assembly in from assembly in AppDomain.CurrentDomain.GetAssemblies()
                            let testClasses = assembly.GetTypes()
                                .Where(t => t.GetCustomAttributes(false).Any(a => a is TestClassAttribute))
                            where testClasses.Any()
                            select assembly)
                        {
                            try
                            {

                                testRunner.Run(assembly);
                            }
                            catch (Exception exception)
                            {

                                Console.WriteLine("Error ejecutando {0}", o);
                                Console.WriteLine(exception);

                            }
                        }
                        break;
                    default:


                        var asembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == o);




                        try
                        {
                            testRunner.Run(asembly);
                        }
                        catch (Exception exception)
                        {

                            Console.WriteLine("Error ejecutando {0}", o);
                            Console.WriteLine(exception);

                        }
                        break;
                }



                Console.Write("Seleciona los test que quieres ejecutar chose " + o + ". Press any key to exit", 21, 22,ConsoleColor.Black,ConsoleColor.White);
                Console.ReadKey();
                menu.Start(menuNames);


            };
            menuNames.Add("Ejecutar Todos");
            menuNames.Add("Salir");
            menu.Start(menuNames);

        }
    }
}