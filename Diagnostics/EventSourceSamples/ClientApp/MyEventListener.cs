using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ClientApp
{
    public class MyEventListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            WriteLine($"created {eventSource.Name} {eventSource.Guid}");
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            WriteLine($"event id: {eventData.EventId} source: {eventData.EventSource.Name}");
            foreach (var payload in eventData.Payload)
            {
                WriteLine($"\t{payload}");
            }
        }
    }

}
