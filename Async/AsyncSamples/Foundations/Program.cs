using System;
using System.Threading;
using System.Threading.Tasks;
#if DNX46
using System.Windows.Threading;
#endif
using static System.Console;

namespace Foundations
{
    public class Program
    {
        public void Main(string[] args)
        {
#if DNX46
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
                case "casync":
                    ConvertingAsyncPattern();
                    break;
                default:
                    Usage();
                    break;
            }

            ConvertingAsyncPattern();
            ReadLine();
        }

        private void Usage()
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
            WriteLine("\t-casync\t\tconvert async pattern");
            WriteLine("\t");
            return;
        }

        private async void ConvertingAsyncPattern()
        {
            string r = await Task<string>.Factory.FromAsync<string>(BeginGreeting, EndGreeting, "Angela", null);
            WriteLine(r);
        }


        private async void MultipleAsyncMethods()
        {
            string s1 = await GreetingAsync("Stephanie");
            string s2 = await GreetingAsync("Matthias");
            WriteLine($"Finished both methods.\n Result 1: {s1}\n Result 2: {s2}");
        }

        private async void MultipleAsyncMethodsWithCombinators1()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            await Task.WhenAll(t1, t2);
            WriteLine($"Finished both methods.\n Result 1: {t1.Result}\n Result 2: {t2.Result}");
        }

        private async void MultipleAsyncMethodsWithCombinators2()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            string[] result = await Task.WhenAll(t1, t2);
            WriteLine($"Finished both methods.\n Result 1: {result[0]}\n Result 2: {result[1]}");
        }

        private void CallerWithContinuationTask()
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



        private void CallerWithAwaiter()
        {
            TraceThreadAndTask("starting CallerWithAwaiter");
            string result = GreetingAsync("Matthias").GetAwaiter().GetResult();
            WriteLine(result);
            TraceThreadAndTask("ending CallerWithAwaiter");
        }

        private async void CallerWithAsync()
        {
            TraceThreadAndTask("started CallerWithAsync");
            string result = await GreetingAsync("Stephanie");
            WriteLine(result);
            TraceThreadAndTask("finished CallerWithAsync");
        }

        private async void CallerWithAsync2()
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

        private Func<string, string> greetingInvoker = Greeting;

        IAsyncResult BeginGreeting(string name, AsyncCallback callback, object state)
        {
            return greetingInvoker.BeginInvoke(name, callback, state);
        }

        string EndGreeting(IAsyncResult ar)
        {
            return greetingInvoker.EndInvoke(ar);
        }

        public static void TraceThreadAndTask(string info)
        {
            string taskInfo = Task.CurrentId == null ? "no task" : "task " + Task.CurrentId;
#if DNX46
            WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} and {taskInfo}");
#else
            WriteLine($"{info} in {taskInfo}");
#endif
        }

    }
}

