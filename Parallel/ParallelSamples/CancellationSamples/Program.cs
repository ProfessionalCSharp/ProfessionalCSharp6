using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace CancellationSamples
{
    class Program
    {
        static void Main()
        {
            // CancelParallelFor();
            CancelTask();
            ReadLine();
        }

        public static void CancelTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => WriteLine("*** task cancelled"));
            // send a cancel after 500 ms
            cts.CancelAfter(500);
            Task t1 = Task.Run(() =>
            {
                WriteLine("in task");
                for (int i = 0; i < 20; i++)
                {
                    Task.Delay(100).Wait();
                    CancellationToken token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        WriteLine("cancelling was requested, " +
                          "cancelling from within the task");
                        token.ThrowIfCancellationRequested();
                        break;
                    }
                    WriteLine("in loop");
                }
                WriteLine("task finished without cancellation");
            }, cts.Token);
            try
            {
                t1.Wait();
            }
            catch (AggregateException ex)
            {
                WriteLine($"exception: {ex.GetType().Name}, {ex.Message}");
                foreach (var innerException in ex.InnerExceptions)
                {
                    WriteLine($"inner exception: {ex.InnerException.GetType()}," +
                      $"{ex.InnerException.Message}");
                }
            }
        }


        public static void CancelParallelFor()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => WriteLine("*** token cancelled"));

            // send a cancel after 500 ms
            cts.CancelAfter(500);

            try
            {
                ParallelLoopResult result =
                  Parallel.For(0, 100, new ParallelOptions
                  {
                      CancellationToken = cts.Token,
                  },
                  x =>
                  {
                      WriteLine($"loop {x} started");
                      int sum = 0;
                      for (int i = 0; i < 100; i++)
                      {
                          Task.Delay(2).Wait();
                          sum += i;
                      }
                      WriteLine($"loop {x} finished");
                  });
            }
            catch (OperationCanceledException ex)
            {
                WriteLine(ex.Message);
            }

        }
    }
}
