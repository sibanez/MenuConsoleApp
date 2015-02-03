using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MenuConsoleApp.Controlers;
using MenuConsoleApp.Controls.ListBox;

namespace DemoMenuConsoleApp.Controlers
{
    [Description("Seleciona Fuente")]
    public class Fuentes : IConsoleControler
    {
        public int Order
        {
            get { return 1; }

        }
        public void Run()
        {
            var menu = ConsoleHelper.ConsoleFonts.Select(f => "Fuente "+ f.Index).ToList();
            Star(menu);
        }

        public void Star(IList<string> menuNames)
        {
            var menu = new ConsoleListBox();
            menu.Changed += (o, eventArgs) =>
            {
                menu.CleanUp();

                switch (o)
                {
                    case "Salir":
                        return;
                    default:
                        var numeroFuente = uint.Parse(o.Replace("Fuente ", ""));
                        var fuente = ConsoleHelper.ConsoleFonts.FirstOrDefault(f => f.Index == numeroFuente);

                        ConsoleHelper.SetConsoleFont(fuente.Index);
                        Console.WriteLine("Esto es una prueba fuente :{0}", fuente.Index);
                        Console.ReadLine();
                        break;
                }

                Console.Write("Seleciona los test que quieres ejecutar chose " + o + ". Press any key to exit", 21, 22, ConsoleColor.Black, ConsoleColor.White);
                Console.ReadKey();
                menu.Start(menuNames);

            };
            
            menuNames.Add("Salir");
            menu.Start(menuNames);

        }
    }
}