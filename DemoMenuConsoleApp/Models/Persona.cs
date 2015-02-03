using System;

namespace DemoMenuConsoleApp.Models
{
    public class Persona
    {
       

        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int Edad { get; internal set; }



    }
}