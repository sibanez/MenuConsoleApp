using System.ComponentModel;

namespace MenuConsoleApp.Controlers
{
    [Description("Salir")]

    public class Salir : IConsoleControler
    {
        public int Order
        {
            get { return 5006; }
            
        }

        public void Run()
        {
            System.Environment.Exit(0);
        }
    }
}