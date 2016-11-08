using System;
#if NET46
using System.Threading;
using System.Windows.Threading;
#endif
using System.Threading.Tasks;
using static System.Console;

namespace Foundations
{
    class Program
    {
        static void Main(string[] args)
        {
#if NET46
            var ctx = new DispatcherSynchronizationContext();

            SynchronizationContext.SetSynchronizationContext(ctx);
#endif
            if (args.Length != 1)
            {
                Usage();
                return;
            }

            switch (args[0].ToLower())
            {
                case "-async":
                    CallerWithAsync();
                    break;
                case "-cont":
                    CallerWithContinuationTask();
                    break;
                case "-awaiter":
                    CallerWithAwaiter();
                    break;
                case "-masync":
                    MultipleAsyncMethods();
                    break;
                case "-comb":
                    MultipleAsyncMethodsWithCombinators1();
                    break;
                case "-comb2":
                    MultipleAsyncMethodsWithCombinators2();
                    break;
#if NET46
                case "-casync":
                    ConvertingAsyncPattern();
                    break;
#endif
                default:
                    Usage();
                    break;
            }

            ReadLine();
        }

        private static void Usage()
        {
            WriteLine("Usage: Foundations Command");
            WriteLine();
            WriteLine("Commands:");
            WriteLine("\t-async\t\tcaller with async");
            WriteLine("\t-cont\t\tcaller with continuation tasks");
            WriteLine("\t-awaiter\t\tcaller with awaiter");
            WriteLine("\t-masync\t\tmultiple async methods");
            WriteLine("\t-comb\t\tmultiple async methods with combinators");
            WriteLine("\t-comb2\t\tmultiple async methods with combinators2");
#if NET46
            WriteLine("\t-casync\t\tconvert async pattern");
#endif
            WriteLine("\t");
            return;
        }

        private static async void ConvertingAsyncPattern()
        {
            string r = await Task<string>.Factory.FromAsync<string>(BeginGreeting, EndGreeting, "Angela", null);
            WriteLine(r);
        }


        private static async void MultipleAsyncMethods()
        {
            string s1 = await GreetingAsync("Stephanie");
            string s2 = await GreetingAsync("Matthias");
            WriteLine($"Finished both methods.\n Result 1: {s1}\n Result 2: {s2}");
        }

        private static async void MultipleAsyncMethodsWithCombinators1()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            await Task.WhenAll(t1, t2);
            WriteLine($"Finished both methods.\n Result 1: {t1.Result}\n Result 2: {t2.Result}");
        }

        private static async void MultipleAsyncMethodsWithCombinators2()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            string[] result = await Task.WhenAll(t1, t2);
            WriteLine($"Finished both methods.\n Result 1: {result[0]}\n Result 2: {result[1]}");
        }

        private static void CallerWithContinuationTask()
        {
            TraceThreadAndTask("started CallerWithContinuationTask");

            var t1 = GreetingAsync("Stephanie");


            t1.ContinueWith(t =>
            {
                string result = t.Result;
                WriteLine(result);

                TraceThreadAndTask("finished CallerWithContinuationTask");
            });
        }



        private static void CallerWithAwaiter()
        {
            TraceThreadAndTask("starting CallerWithAwaiter");
            string result = GreetingAsync("Matthias").GetAwaiter().GetResult();
            WriteLine(result);
            TraceThreadAndTask("ending CallerWithAwaiter");
        }

        private static async void CallerWithAsync()
        {
            TraceThreadAndTask("started CallerWithAsync");
            string result = await GreetingAsync("Stephanie");
            WriteLine(result);
            TraceThreadAndTask("finished CallerWithAsync");
        }

        private static async void CallerWithAsync2()
        {
            TraceThreadAndTask("started {nameof(CallerWithAsync2)}");
            WriteLine(await GreetingAsync("Stephanie"));
            TraceThreadAndTask("finished CallerWithAsync2");
        }

        static Task<string> GreetingAsync(string name)
        {
            return Task.Run<string>(() =>
            {
                TraceThreadAndTask("running GreetAsync");
                return Greeting(name);
            });
        }

        static string Greeting(string name)
        {
            TraceThreadAndTask("running Greeting");
            Task.Delay(3000).Wait();
            return $"Hello, {name}";
        }

        private static Func<string, string> greetingInvoker = Greeting;

        static IAsyncResult BeginGreeting(string name, AsyncCallback callback, object state)
        {
            return greetingInvoker.BeginInvoke(name, callback, state);
        }

        static string EndGreeting(IAsyncResult ar)
        {
            return greetingInvoker.EndInvoke(ar);
        }

        public static void TraceThreadAndTask(string info)
        {
            string taskInfo = Task.CurrentId == null ? "no task" : "task " + Task.CurrentId;
#if NET46
            WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} and {taskInfo}");
#else
            WriteLine($"{info} in {taskInfo}");
#endif
        }

    }
}