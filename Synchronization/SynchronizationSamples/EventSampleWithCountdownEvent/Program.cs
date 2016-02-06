using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace EventSample
{
    class Program
    {
        static void Main()
        {
            const int taskCount = 4;

            var cEvent = new CountdownEvent(taskCount);
            var calcs = new Calculator[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                calcs[i] = new Calculator(cEvent);
                int i1 = i;
                Task.Run(() => calcs[i1].Calculation(i1 + 1, i1 + 3));
            }

            cEvent.Wait();
            WriteLine("all finished");

            for (int i = 0; i < taskCount; i++)
            {
                WriteLine($"task for {i}, result: {calcs[i].Result}");
            }
        }
    }
}
