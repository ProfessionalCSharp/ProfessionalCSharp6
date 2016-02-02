using System;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;

namespace EventSourceSampleInheritance
{
    class Program
    {
        static void Main()
        {
            SampleEventSource.Log.Startup();
            WriteLine($"Log Guid: {SampleEventSource.Log.Guid}");
            WriteLine($"Name: {SampleEventSource.Log.Name}");
            NetworkRequestSample().Wait();
            //ActivitySample();
            ReadLine();
        }

        private static async Task NetworkRequestSample()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://www.cninnovation.com";
                    SampleEventSource.Log.CallService(url);
                    string result = await client.GetStringAsync(url);
                    SampleEventSource.Log.CalledService(url, result.Length);
                }
                WriteLine("Complete.................");
            }
            catch (Exception ex)
            {
                SampleEventSource.Log.ServiceError(ex.Message, ex.HResult);
                WriteLine(ex.Message);
            }
        }

        //private static void ActivitySample()
        //{
        //    Parallel.For(0, 20, x =>
        //    {
        //        SampleEventSource.Log.ActionStart(x);
        //        Task.Delay(10).Wait();
        //        SampleEventSource.Log.Action(x);
        //        Task.Delay(10).Wait();
        //        SampleEventSource.Log.ActionStop(x);
        //    });
        //    WriteLine("Activity complete");
        //}

        //private static void ProcessTask(int x)
        //{
        //    SampleEventSource.Log.ProcessingStart(x);
        //    var client = new HttpClient()

        //    SampleEventSource.Log.ProcessingStop(x);
        //}
    }

}
