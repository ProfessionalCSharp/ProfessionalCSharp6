using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ParallelLinqSample
{
    public class Program
    {
        public static void Main()
        {
            IList<int> data = SampleData();
            LinqQuery(data);
            ExtensionMethods(data);
            UseAPartitioner(data);
            UseCancellation(data);
        }

        private static void LinqQuery(IEnumerable<int> data)
        {
            WriteLine(nameof(LinqQuery));
            var res = (from x in data.AsParallel()
                       where Math.Log(x) < 4
                       select x).Average();
            WriteLine($"result from {nameof(LinqQuery)}: {res}");
            WriteLine();
        }

        private static void ExtensionMethods(IEnumerable<int> data)
        {
            WriteLine(nameof(ExtensionMethods));
            var res = data.AsParallel()
                .Where(x => Math.Log(x) < 4)
                .Select(x => x).Average();

            WriteLine($"result from {nameof(ExtensionMethods)}: {res}");
            WriteLine();
        }

        private static void UseAPartitioner(IList<int> data)
        {
            WriteLine(nameof(UseAPartitioner));
            var res = (from x in Partitioner.Create(data, loadBalance: true).AsParallel()
                       where Math.Log(x) < 4
                       select x).Average();

            WriteLine($"result from {nameof(UseAPartitioner)}: {res}");
            WriteLine();
        }

        private static void UseCancellation(IEnumerable<int> data)
        {
            WriteLine(nameof(UseCancellation));
            var cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    var res = (from x in data.AsParallel().WithCancellation(cts.Token)
                               where Math.Log(x) < 4
                               select x).Average();

                    WriteLine($"query finished, sum: {res}");
                }
                catch (OperationCanceledException ex)
                {
                    WriteLine(ex.Message);
                }
            });

            WriteLine("query started");
            Write("cancel? ");
            string input = ReadLine();
            if (input.ToLower().Equals("y"))
            {
                cts.Cancel();
            }

            WriteLine();
        }

        static IList<int> SampleData()
        {
            const int arraySize = 50000000;
            var r = new Random();
            return Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
        }
    }
}
