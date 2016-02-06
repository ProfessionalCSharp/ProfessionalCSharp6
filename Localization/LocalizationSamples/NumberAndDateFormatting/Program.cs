using System;
using System.Globalization;
using static System.Console;

namespace NumberAndDateFormatting
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            switch (args[0])
            {
                case "-n":
                    NumberFormatDemo();
                    break;
                case "-d":
                    DateFormatDemo();
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        private static void ShowUsage()
        {
            WriteLine("NumberAndDateFormatting command");
            WriteLine("\tCommands:");
            WriteLine("\t-n\tShow numbers");
            WriteLine("\t-d\tShow dates");
        }

        public static void NumberFormatDemo()
        {
            int val = 1234567890;

            // culture of the current thread
            WriteLine(val.ToString("N"));

            // use IFormatProvider
            WriteLine(val.ToString("N", new CultureInfo("fr-FR")));

            // change the culture of the thread
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            
            WriteLine(val.ToString("N"));
        }

        public static void DateFormatDemo()
        {
            var d = new DateTime(2015, 09, 27);

            // current culture
            WriteLine(d.ToString("D"));

            // use IFormatProvider
            WriteLine(d.ToString("D", new CultureInfo("fr-FR")));

            // use current culture
            WriteLine($"{CultureInfo.CurrentCulture}: {d:D}");

            CultureInfo.CurrentCulture = new CultureInfo("es-ES");
            WriteLine($"{CultureInfo.CurrentCulture}: {d:D}");
        }

    }
}
