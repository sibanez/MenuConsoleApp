using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuConsoleApp.Controlers
{
    internal static class ControlersFactory
    {
        public static IEnumerable<IConsoleControler> CreateControlers()
        {
            var controlers = new List<IConsoleControler>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {


                controlers.AddRange(assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && typeof(IConsoleControler).IsAssignableFrom(t))
                    .Select(Activator.CreateInstance)
                    .Cast<IConsoleControler>()
                    .OrderBy(b => b.Order));

            }

            return controlers;
        }


      
           
    }
}