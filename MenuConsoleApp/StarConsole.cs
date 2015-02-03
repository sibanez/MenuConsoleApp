using System;
using System.ComponentModel;
using System.Linq;
using MenuConsoleApp.Controlers;
using MenuConsoleApp.Controls.ListBox;

namespace MenuConsoleApp
{
    public static class StarConsole
    {
        public static ConsoleListBox Menu { get; internal set; }

        public static void Star(string[] args)
        {

            Console.BufferHeight = 1000;
            Console.BufferWidth = 1000;

            ConsoleHelper.SetConsoleFont(9);
       

            var controlers = ControlersFactory.CreateControlers();

            var consoleControlers = controlers as IConsoleControler[] ?? controlers.ToArray();
            var menus =
                consoleControlers.Select(c => c.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false))
                    .Select(d => ((DescriptionAttribute)d[0]).Description)
                    .ToArray();


            var menu = new ConsoleListBox();
            menu.Changed += (o, eventArgs) =>
            {
                menu.CleanUp();




                foreach (var consoleControler in
                    consoleControlers.Select(
                        consoleControler =>
                            new
                            {
                                consoleControler,
                                ca =
                                    consoleControler.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false)
                            })
                        .SelectMany(
                            @t =>
                                @t.ca.OfType<DescriptionAttribute>()
                                    .Select(ss => ss.Description)
                                    .Where(descripcion => descripcion == o), (@t, descripcion) => @t.consoleControler))
                {
                    try
                    {
                        consoleControler.Run();
                    }
                    catch (Exception exception)
                    {
                        
                        Console.WriteLine("Error ejecutando {0}",o);
                        Console.WriteLine(exception);

                    }
                    
                }


                Console.Write("You chose " + o + ". Press any key to exit", 21, 22, ConsoleColor.Black,
                    ConsoleColor.White);
                Console.ReadKey();

                menu.Start(menus);


            };
            menu.Start(menus);
            
        }
    }
}