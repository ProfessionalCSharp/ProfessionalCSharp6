using System;
using System.Threading.Tasks;
using static System.Console;

namespace ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Usage();
                return;
            }

            switch (args[0].ToLower())
            {
                case "-donthandle":
                    DontHandle();
                    break;
                case "-handle":
                    HandleOneError();
                    break;
                case "-two":
                    StartTwoTasks();
                    break;
                case "-twop":
                    StartTwoTasksParallel();
                    break;
                case "-agg":
                    ShowAggregatedException();
                    break;
                default:
                    Usage();
                    break;
            }

            ReadLine();
        }

        public static void Usage()
        {
            WriteLine("Usage: ErrorHandling Command");
            WriteLine();
            WriteLine("Commands:");
            WriteLine("\t-donthandle\t\tcall async methods with exceptions not caught");
            WriteLine("\t-two\t\tstart two tasks");
            WriteLine("\t-twop\t\tstart two tasks parallel");
            WriteLine("\t-agg\t\taggregated exception");
            WriteLine("\t");
        }

        private static async void ShowAggregatedException()
        {
            Task taskResult = null;
            try
            {
                Task t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await (taskResult = Task.WhenAll(t1, t2));
            }
            catch (Exception ex)
            {
                // just display the exception information of the first task that is awaited within WhenAll
                WriteLine($"handled {ex.Message}");
                foreach (var ex1 in taskResult.Exception.InnerExceptions)
                {
                    WriteLine($"inner exception {ex1.Message} from task {ex1.Source}");
                }
            }
        }

        private async static void StartTwoTasksParallel()
        {
            Task t1 = null;
            try
            {
                t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await Task.WhenAll(t1, t2);
            }
            catch (Exception ex)
            {
                // just display the exception information of the first task that is awaited within WhenAll
                WriteLine($"handled {ex.Message}");
            }
        }

        private static async void StartTwoTasks()
        {
            try
            {
                await ThrowAfter(2000, "first");
                await ThrowAfter(1000, "second"); // the second call is not invoked because the first method throws an exception
            }
            catch (Exception ex)
            {
                WriteLine("handled {0}", ex.Message);
            }
        }

        private static void DontHandle()
        {
            try
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                ThrowAfter(200, "first");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                              // Exception is not caught because the exception is assigned to the task which is not awaited
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private static async void HandleOneError()
        {
            try
            {
                await ThrowAfter(2000, "first");
            }
            catch (Exception ex)
            {
                WriteLine($"handled {ex.Message}");
            }
        }

        static async Task ThrowAfter(int ms, string message)
        {
            await Task.Delay(ms);
            throw new Exception(message);
        }
    }
}
