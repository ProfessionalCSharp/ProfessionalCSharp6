using System;
using static System.Console;

namespace EnumSample
{
    class Program
    {
        static void Main()
        {

            DaysOfWeekSamples();
            ColorSamples();
            UsingEnumClass();

            ReadLine();
        }

        private static void UsingEnumClass()
        {
            Color red;
            if (Enum.TryParse<Color>("Red", out red))
            {
                WriteLine($"successfully parsed {red}");
            }

            string redtext = Enum.GetName(typeof(Color), red);
            WriteLine(redtext);

            foreach (var day in Enum.GetNames(typeof(Color)))
            {
                WriteLine(day);
            }


            foreach (short val in Enum.GetValues(typeof(Color)))
            {
                WriteLine(val);
            }

            foreach (var item in Enum.GetValues(typeof(Color)))
            {
                WriteLine(item);
            }

        }

        private static void DaysOfWeekSamples()
        {
            DaysOfWeek mondayAndWednesday = DaysOfWeek.Monday | DaysOfWeek.Wednesday;
            WriteLine(mondayAndWednesday);
            DaysOfWeek weekend = DaysOfWeek.Saturday | DaysOfWeek.Sunday;
            WriteLine(weekend);
            DaysOfWeek workday = DaysOfWeek.Monday | DaysOfWeek.Tuesday | DaysOfWeek.Wednesday | DaysOfWeek.Thursday  | DaysOfWeek.Friday;
            WriteLine(workday);
        }

        private static void ColorSamples()
        {

            Color c1 = Color.Red;
            WriteLine(c1);

            Color c2 = (Color)2;
            WriteLine(c2);
            WriteLine((short)c2);
        }
    }
}
