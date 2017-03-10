using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Server;
using static System.Console;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                ShowUsage();
                return;
            }
            StartServerAsync(args).Wait();
            ReadLine();
        }

        private static void ShowUsage()
        {
            WriteLine("Usage: HttpServer Prefix [Prefix2] [Prefix3] [Prefix4]");
        }

        private static string htmlFormat =
            "<!DOCTYPE html><html><head><title>{0}</title></head>" +
            "<body>{1}</body></html>";

        public static async Task StartServerAsync(params string[] prefixes)
        {
            try
            {
             
                WriteLine($"server starting at");
                var listener = new WebListener();
              
                foreach (var prefix in prefixes)
                {
                    listener.Settings.UrlPrefixes.Add(prefix);
                    WriteLine($"\t{prefix}");
                }

                listener.Start();

                do
                {
                    using (RequestContext context = await listener.AcceptAsync())
                    {
                        context.Response.Headers.Add("content-type", new string[] { "text/html" });
                        context.Response.StatusCode = (int)HttpStatusCode.OK;

                        byte[] buffer = GetHtmlContent(context.Request);
                        await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                    }

                } while (true);

            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private static byte[] GetHtmlContent(Request request)
        {
            string title = "Sample WebListener";

            var sb = new StringBuilder("<h1>Hello from the server</h1>");
            sb.Append("<h2>Header Info</h2>");
            sb.Append(string.Join(" ", GetHeaderInfo(request.Headers)));
            sb.Append("<h2>Request Object Information</h2>");
            sb.Append(string.Join(" ", GetRequestInfo(request)));
        
            string html = string.Format(htmlFormat, title, sb.ToString());
            return Encoding.UTF8.GetBytes(html);
        }

        private static IEnumerable<string> GetRequestInfo(Request request) =>
            request.GetType().GetProperties().Select(p => $"<div>{p.Name}: {p.GetValue(request)}</div>");


        private static IEnumerable<string> GetHeaderInfo(HeaderCollection headers) =>
            headers.Keys.Select(key => $"<div>{key}: {string.Join(",", headers.GetValues(key))}</div>");
    }
}
