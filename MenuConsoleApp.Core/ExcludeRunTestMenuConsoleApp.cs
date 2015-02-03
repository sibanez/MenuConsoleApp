using System;

namespace MenuConsoleApp.Core
{
    [AttributeUsage(AttributeTargets.Class |
         AttributeTargets.Method,
         AllowMultiple = true)]
    public class ExcludeRunTestMenuConsoleAppAttribute : Attribute{
    }
}
