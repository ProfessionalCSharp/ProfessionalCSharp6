using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;

namespace EventSourceSampleAnnotations
{
    class Program
    {
        public static void GenerateManifest()
        {
            string schema = SampleEventSource.GenerateManifest(
              typeof(SampleEventSource), ".");
            File.WriteAllText("sampleeventsource.xml", schema);
        }


        static void Main()
        {
            SampleEventSource.Log.Startup();
            GenerateManifest();
            WriteLine($"Log Guid: {SampleEventSource.Log.Guid}");
            WriteLine($"Name: {SampleEventSource.Log.Name}");
            NetworkRequestSample().Wait();
            ReadLine();
        }

        private static async Task NetworkRequestSample()
        {
            try
            {
                var client = new HttpClient();
                string url = "http://www.cninnovation.com";
                SampleEventSource.Log.CallService(url);
                string result = await client.GetStringAsync(url);
                SampleEventSource.Log.CalledService(url, result.Length);
                WriteLine("Complete.................");
            }
            catch (Exception ex)
            {
                SampleEventSource.Log.ServiceError(ex.Message, ex.HResult);
                WriteLine(ex.Message);
            }
        }
    }
}
