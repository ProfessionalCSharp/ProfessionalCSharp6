using System;
using System.Threading.Tasks;
using static System.Console;

namespace AsyncDelegate
{
    public delegate int TakesAWhileDelegate(int x, int ms);

    class Program
    {
        static void Main()
        {
            try
            {
                TakesAWhileDelegate d1 = TakesAWhile;

                IAsyncResult ar = d1.BeginInvoke(1, 3000, null, null);
                while (true)
                {
                    Write(".");
                    if (ar.AsyncWaitHandle.WaitOne(50))
                    {
                        WriteLine("Can get the result now");
                        break;
                    }
                }
                int result = d1.EndInvoke(ar);
                WriteLine($"result: {result}");
            }
            catch (PlatformNotSupportedException)
            {
                WriteLine("PlatformNotSupported exception - with async delegates please use the full .NET Framework");
            }
        }

        public static int TakesAWhile(int x, int ms)
        {
            Task.Delay(ms).Wait();
            return 42;
        }
    }
}
