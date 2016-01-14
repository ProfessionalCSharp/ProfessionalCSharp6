using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ParallelSamples
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
                case "pf":
                    ParallelFor();
                    break;
                case "pfa":
                    ParallelForWithAsync();
                    break;
                case "spfe":
                    StopParallelForEarly();
                    break;
                case "pfi":
                    ParallelForWithInit();
                    break;
                case "pfe":
                    ParallelForEach();
                    break;
                case "pi":
                    ParallelInvoke();
                    break;
                default:
                    ShowUsage();
                    break;
            }

            ReadLine();
        }

        private static void ShowUsage()
        {
            WriteLine("ParallelSamples options");
            WriteLine("Options:");
            WriteLine("\t-pf\tParallel For");
            WriteLine("\t-pfa\tParallel For Async");
            WriteLine("\t-spfe\tStop Parallel For Early");
            WriteLine("\t-pfwi\tParallel For With Init");
            WriteLine("\t-pfe\tParallel ForEach");
            WriteLine("\t-pi\tParallel Invoke");
        }

        public static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar);
        }

        private static void Foo()
        {
            WriteLine("foo");
        }

        private static void Bar()
        {
            WriteLine("bar");
        }


        public static void ParallelForEach()
        {
            string[] data = {"zero", "one", "two", "three", "four", "five",
                "six", "seven", "eight", "nine", "ten", "eleven", "twelve"};
            ParallelLoopResult result =
              Parallel.ForEach<string>(data, s =>
              {
                  WriteLine(s);
              });

        }

        public static void ParallelFor()
        {
            ParallelLoopResult result =
              Parallel.For(0, 10, i =>
              {
                  Log($"S {i}");
                  Task.Delay(10).Wait();
                  Log($"E {i}");
              });
            WriteLine($"Is completed: {result.IsCompleted}");
        }

        public static void ParallelForWithAsync()
        {
            ParallelLoopResult result =
              Parallel.For(0, 10, async i =>
              {
                  Log($"S {i}");
                  await Task.Delay(10);
                  Log($"E {i}");
              });
            WriteLine($"Is completed: {result.IsCompleted}");
        }

        public static void StopParallelForEarly()
        {
            ParallelLoopResult result =
              Parallel.For(10, 40, (int i, ParallelLoopState pls) =>
              {
                  Log($"S {i}");
                  if (i > 12)
                  {
                      pls.Break();
                      Log($"break now {i}");
                  }
                  Task.Delay(10).Wait();
                  Log($"E {i}");
              });

            WriteLine($"Is completed: {result.IsCompleted}");
            WriteLine($"lowest break iteration: {result.LowestBreakIteration}");

        }

        public static void ParallelForWithInit()
        {
            Parallel.For<string>(0, 100, () =>
            {
                // invoked once for each thread
                Log("init thread");
                return $"t{Thread.CurrentThread.ManagedThreadId}";
            },
            (i, pls, str1) =>
            {
                // invoked for each member
                Log($"body i: {i} str1: {str1}");
                Task.Delay(10).Wait();
                return $"i {i}";
            },
            (str1) =>
            {
                // final action on each thread
                Log($"finally {str1}");
            });
        }


        public static void Log(string prefix)
        {
            WriteLine($"{prefix} task: {Task.CurrentId}, thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
