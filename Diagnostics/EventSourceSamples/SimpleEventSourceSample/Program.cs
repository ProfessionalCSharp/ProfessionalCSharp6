using System;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;

namespace SimpleEventSourceSample
{
    class Program
    {
        private static EventSource sampleEventSource = new EventSource("Wrox-SimpleEventSourceSample");
        static void Main()
        {
            WriteLine($"Log Guid: {sampleEventSource.Guid}");
            WriteLine($"Name: {sampleEventSource.Name}");

            sampleEventSource.Write("Startup", new { Info = "started app" });
            NetworkRequestSample().Wait();
            ReadLine();
            sampleEventSource.Dispose();
        }

        private static async Task NetworkRequestSample()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://www.cninnovation.com";
                    sampleEventSource.Write("Network", new { Info = $"calling {url}" });

                    string result = await client.GetStringAsync(url);
                    sampleEventSource.Write("Network", new { Info = $"completed call to {url}, result string length: {result.Length}" });
                }
                WriteLine("Complete.................");
            }
            catch (Exception ex)
            {
                sampleEventSource.Write("Network Error", new EventSourceOptions { Level = EventLevel.Error }, new { Message = ex.Message, Result = ex.HResult });
                WriteLine(ex.Message);
            }
        }
    }
}
