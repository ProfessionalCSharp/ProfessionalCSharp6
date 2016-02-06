using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace TimersSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadingTimer();
        }


        private static void ThreadingTimer()
        {
            using (var t1 = new Timer(
               TimeAction, null, TimeSpan.FromSeconds(2),
               TimeSpan.FromSeconds(3)))
            {

                Task.Delay(15000).Wait();
            }
        }


        private static void TimeAction(object o)
        {
            WriteLine($"System.Threading.Timer {DateTime.Now:T}");
        }
    }
}
