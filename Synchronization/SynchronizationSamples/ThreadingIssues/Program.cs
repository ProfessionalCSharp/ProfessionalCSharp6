using System.Threading.Tasks;
using static System.Console;

namespace ThreadingIssues
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

            switch (args[0].ToLower())
            {
                case "-d":
                    Deadlock();
                    break;
                case "-r":
                    RaceConditions();
                    break;
                default:
                    ShowUsage();
                    break;
            }

            ReadLine();
        }

        private static void ShowUsage()
        {
            WriteLine($"Usage: ThreadingIssues options");
            WriteLine();
            WriteLine("options:");
            WriteLine("\t-d\tCreate a deadlock");
            WriteLine("\t-r\tCreate a race condition");
        }

        public static void RaceConditions()
        {
            var state = new StateObject();
            for (int i = 0; i < 2; i++)
            {
                Task.Run(() => new SampleTask().RaceCondition(state));
            }
        }

        public static void Deadlock()
        {
            var s1 = new StateObject();
            var s2 = new StateObject();
            Task.Run(() => new SampleTask(s1, s2).Deadlock1());
            Task.Run(() => new SampleTask(s1, s2).Deadlock2());

            Task.Delay(10000).Wait();

        }
    }
}
