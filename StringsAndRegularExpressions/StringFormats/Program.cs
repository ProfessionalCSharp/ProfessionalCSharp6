using System;
using System.Globalization;
using static System.Console;
using static System.FormattableString;

namespace StringFormats
{

    public class Program
    {
        static void Main()
        {
            FormattedStringDemo();
            DateAndNumbers();
            FormattedStringDemo();
            MoreFormattableString();
            UseCustomIFormattable();
            ReadLine();
        }

        static void UseCustomIFormattable()
        {
            var p1 = new Person { FirstName = "Stephanie", LastName = "Nagel" };
            WriteLine(p1.ToString("F"));
            WriteLine($"{p1:F}");
        }

        public static void FormattedStringDemo()
        {
            DateTime today = DateTime.Today;

            string s = "Hello";
            WriteLine($"{{s}} displays the value of s: {s}");

            string formatString = $"{s}, {{0}}";
            string s2 = "World";

            WriteLine(formatString, s2);
            WriteLine($"{today:D}");

            var day = new DateTime(2025, 2, 14);
            WriteLine($"{day:d}");
            WriteLine(Invariant($"{day:d}"));

            string a1 = "a";
            int a2 = 3;
            double a3 = 3.4;
            ShowDetails($"a1: {a1} a2: {a2} a3: {a3}");
        }

        public static void MoreFormattableString()
        {
            int x = 3, y = 4;
            FormattableString s = $"The result of {x} + {y} is {x + y}";
            WriteLine($"format: {s.Format}");
            for (int i = 0; i < s.ArgumentCount; i++)
            {
                WriteLine($"argument {i}: {s.GetArgument(i)}");
            }

        }


        //public static string Invariant(FormattableString s) =>
        //    s.ToString(CultureInfo.InvariantCulture);
        


        public static void ShowDetails(FormattableString s)
        {
            WriteLine($"argument count: {s.ArgumentCount}");
            WriteLine($"format: {s.Format}");
            for (int i = 0; i < s.ArgumentCount; i++)
            {
                WriteLine($"Argument {i}: {s.GetArgument(i)}");
            }

        }

        public static void DateAndNumbers()
        {
            var now = new DateTime(2025, 2, 1);
            WriteLine($"{now:D}");
            WriteLine($"{now:d}");
            WriteLine($"{now:dd-MMM-yyyy}");

            int i = 2477;
            WriteLine($"{i:n} {i:e} {i:x} {i:c}");
            double d = 3.1415;
            WriteLine($"{d:###.###}");
            WriteLine($"{d:000.000}");
            WriteLine($"{i:n2}");
        }
    }
}
