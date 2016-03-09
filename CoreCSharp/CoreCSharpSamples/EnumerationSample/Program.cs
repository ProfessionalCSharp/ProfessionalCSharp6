using static System.Console;

namespace EnumerationSample
{
    public enum TimeOfDay
    {
        Morning = 0,
        Afternoon = 1,
        Evening = 2
    }

    class Program
    {
        static void Main()
        {
            WriteGreeting(TimeOfDay.Morning);
        }

        static void WriteGreeting(TimeOfDay timeOfDay)
        {
            switch (timeOfDay)
            {
                case TimeOfDay.Morning:
                    WriteLine("Good morning!");
                    break;
                case TimeOfDay.Afternoon:
                    WriteLine("Good afternoon!");
                    break;
                case TimeOfDay.Evening:
                    WriteLine("Good evening!");
                    break;
                default:
                    WriteLine("Hello!");
                    break;
            }
        }

    }
}
