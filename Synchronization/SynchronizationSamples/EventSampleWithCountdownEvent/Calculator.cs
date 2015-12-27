using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace EventSample
{
    public class Calculator
    {
        private CountdownEvent _cEvent;

        public int Result { get; private set; }

        public Calculator(CountdownEvent ev)
        {
            _cEvent = ev;
        }

        public void Calculation(int x, int y)
        {
            WriteLine($"Task {Task.CurrentId} starts calculation");
            Task.Delay(new Random().Next(3000)).Wait();
            Result = x + y;

            // signal the event-completed!
            WriteLine($"Task {Task.CurrentId} is ready");
            _cEvent.Signal();
        }
    }

}
