using System;
using System.ComponentModel;
using MenuConsoleApp.Controlers;

namespace DemoMenuConsoleApp.Controlers
{
    [Description("NotImplementedException")]
    public class Prueba : IConsoleControler
    {
        public int Order
        {
            get { return 2; }

        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}