using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace HttpClientSample
{
    class Program
    {
        private const string NorthwindUrl = "http://services.odata.org/Northwind/Northwind.svc/Regions";
        private const string IncorrectUrl = "http://services.odata.org/Northwind1/Northwind.svc/Regions";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            switch (args[0].ToLower())
            {
                case "-s":
                    GetDataSimpleAsync().Wait();
                    break;
                case "-a":
                    GetDataAdvancedAsync().Wait();
                    break;
                case "-e":
                    GetDataWithExceptionsAsync().Wait();
                    break;
                case "-h":
                    GetDataWithHeadersAsync().Wait();
                    break;
                case "-mh":
                    GetDataWithMessageHandlerAsync().Wait();
                    break;
                default:
                    ShowUsage();
                    break;
            }
            ReadLine();
        }

        private static void ShowUsage()
        {
            WriteLine("Usage: HttpClientSample command");
            WriteLine("commands:");
            WriteLine("\t-s\tSimple");
            WriteLine("\t-a\tAdvanced");
            WriteLine("\t-e\tUsing Exceptions");
            WriteLine("\t-h\tWith Headers");
            WriteLine("\t-mh\tWith message handler");
        }

        public static async Task GetDataSimpleAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(NorthwindUrl);
                if (response.IsSuccessStatusCode)
                {
                    WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    WriteLine();
                    WriteLine(responseBodyAsText);
                }
            }
        }

        public static async Task GetDataAdvancedAsync()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, NorthwindUrl);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    WriteLine();
                    WriteLine(responseBodyAsText);
                }
            }
        }

        public static async Task GetDataWithExceptionsAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                    ShowHeaders("Request Headers:", client.DefaultRequestHeaders);
                    HttpResponseMessage response = await client.GetAsync(IncorrectUrl);
                    response.EnsureSuccessStatusCode();

                    ShowHeaders("Response Headers:", response.Headers);

                    WriteLine($"Response Status Code: {response.StatusCode} {response.ReasonPhrase}");
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    WriteLine();
                    WriteLine(responseBodyAsText);

                }
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.Message}");
            }
        }

        public static async Task GetDataWithHeadersAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                    ShowHeaders("Request Headers:", client.DefaultRequestHeaders);

                    HttpResponseMessage response = await client.GetAsync(NorthwindUrl);
                    response.EnsureSuccessStatusCode();

                    ShowHeaders("Response Headers:", response.Headers);

                    WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    WriteLine();
                    WriteLine(responseBodyAsText);

                }
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.Message}");
            }
        }

        public static void ShowHeaders(string title, HttpHeaders headers)
        {
            WriteLine(title);
            foreach (var header in headers)
            {
                string value = string.Join(" ", header.Value);
                WriteLine($"Header: {header.Key} Value: {value}");
            }
            WriteLine();
        }

        public static async Task GetDataWithMessageHandlerAsync()
        {
            try
            {
                using (var client = new HttpClient(new SampleMessageHandler("error")))
                {    
                    client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                    ShowHeaders("Request Headers:", client.DefaultRequestHeaders);

                    HttpResponseMessage response = await client.GetAsync(NorthwindUrl);
                    response.EnsureSuccessStatusCode();

                    ShowHeaders("Response Headers:", response.Headers);

                    WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    WriteLine();
                    WriteLine(responseBodyAsText);

                }
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.Message}");
            }
        }
    }  
}