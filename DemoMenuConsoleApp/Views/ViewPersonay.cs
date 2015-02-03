using System;

namespace DemoMenuConsoleApp.Views
{
    public class ViewPersonay
    {
        public ViewPersonay()
        {
           
            GetValues();
        }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        private void GetValues()
        {
            Console.WriteLine("Entra el nombre de la persona");
            Nombre =Console.ReadLine();

            Console.WriteLine("Entra la fecha de nacimiento");

            FechaNacimiento = DateTime.Parse(Console.ReadLine());
        }
        public void MuestraNombreEdad()
        {
            Console.WriteLine("{0} tiene {1} años ", Nombre,Edad);
            
            Console.ReadKey();
        }


    }
}