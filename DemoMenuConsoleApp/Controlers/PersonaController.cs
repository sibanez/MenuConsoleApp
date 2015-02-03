using System;
using System.ComponentModel;
using DemoMenuConsoleApp.Models;
using DemoMenuConsoleApp.Views;
using MenuConsoleApp.Controlers;

namespace DemoMenuConsoleApp.Controlers
{
    [Description("Opcion con entrada de datos")]
    public class PersonaController :IConsoleControler
    {
       
        private Persona _persona;
        private ViewPersonay _view;
        
        public int Order
        {
            get { return 3; }
        }

        public void Run()
        {
            _view = new ViewPersonay();
            _persona = new Persona
            {
                Nombre = _view.Nombre,
                FechaNacimiento = _view.FechaNacimiento,
              
            };

            _view.Edad = CalculaEdad();
            
            _view.MuestraNombreEdad();
        }

        public int CalculaEdad()
        {
            var today = DateTime.Today;
            return today.Year - _persona.FechaNacimiento.Year;

        }
    }
}