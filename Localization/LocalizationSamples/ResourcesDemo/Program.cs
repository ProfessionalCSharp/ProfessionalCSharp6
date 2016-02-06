using System.Globalization;
using System.Reflection;
using System.Resources;
using static System.Console;

namespace ResourcesDemo
{
    class Program
    {
        static void Main()
        {
            var resources = new ResourceManager("ResourcesDemo.Resources.Messages", typeof(Program).GetTypeInfo().Assembly);
            string goodMorning = resources.GetString("GoodMorning", new CultureInfo("de-AT"));
            WriteLine(goodMorning);

            var programResources = new ResourceManager(typeof(Program));
            WriteLine(programResources.GetString("Resource1"));

        }
    }
}
