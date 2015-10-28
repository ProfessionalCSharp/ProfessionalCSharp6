using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Primitives;
using Microsoft.Framework.WebEncoders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebSampleApp.Controllers;
using WebSampleApp.Middleware;
using WebSampleApp.Services;

namespace WebSampleApp
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISampleService, DefaultSampleService>();
            services.AddTransient<HomeController>();
            services.AddCaching();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(10));
        }

        private const string SessionVisits = "visits";
        private const string SessionTimeCreated = "timecreated";

        public void Configure(IApplicationBuilder app)
        {
            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();
            app.UseSession();
            app.UseHeaderMiddleware();
            app.UseHeading1Middleware();
            app.UseStaticFiles();

            app.Map("/home", homeApp =>
            {
                homeApp.Run(async context =>
                {
                    HomeController controller = app.ApplicationServices.GetService<HomeController>();
                    int statusCode = await controller.Index(context);
                    context.Response.StatusCode = statusCode;
                });
            });

            app.Map("/session", sessionApp =>
            {
                sessionApp.Run(async context =>
                {
                    int visits = context.Session.GetInt32(SessionVisits) ?? 0;
                    string timeCreated = context.Session.GetString(SessionTimeCreated) ?? string.Empty;
                    if (string.IsNullOrEmpty(timeCreated))
                    {
                        timeCreated = DateTime.Now.ToString("t", CultureInfo.InvariantCulture);
                        context.Session.SetString(SessionTimeCreated, timeCreated);
                    }
                    DateTime timeCreated2 = DateTime.Parse(timeCreated);
                    context.Session.SetInt32(SessionVisits, ++visits);
                    await context.Response.WriteAsync($"Number of visits within this session: {visits} that was created at {timeCreated2:T}; current time: {DateTime.Now:T}");
                });
            });

            app.Run(async (context) =>
            {
                // await context.Response.WriteAsync("Hello World!");
                // await context.Response.WriteAsync(GetRequestInformation(context.Request));

                if (context.Request.Path.Value.ToLower() == "/home")
                {
                    HomeController controller = app.ApplicationServices.GetService<HomeController>();
                    int statusCode = await controller.Index(context);
                    context.Response.StatusCode = statusCode;
                    return;
                }

                string result = string.Empty;
                switch (context.Request.Path.Value.ToLower())
                {
                    case "/header":
                        result = GetHeaderInformation(context.Request);
                        break;
                    case "/add":
                        result = Add(context.Request);
                        break;
                    case "/content":
                        result = Content(context.Request);
                        break;
                    case "/form":
                        result = GetForm(context.Request);
                        break;
                    case "/writecookie":
                        result = WriteCookie(context.Response);
                        break;
                    case "/readcookie":
                        result = ReadCookie(context.Request);
                        break;
                    case "/json":
                        result = GetJson(context.Response);
                        break;
                    default:
                        result = GetRequestInformation(context.Request);
                        break;
                }

                await context.Response.WriteAsync(result);

            });
        }

        private string GetJson(HttpResponse response)
        {
            var b = new
            {
                Title = "Professional C# 6",
                Publisher = "Wrox Press",
                Author = "Christian Nagel"
            };

            string json = JsonConvert.SerializeObject(b);
            response.ContentType = "application/json";
            return json;
        }

        private string GetForm(HttpRequest request)
        {
            string result = string.Empty;
            switch (request.Method)
            {
                case "GET":
                    result = GetForm();
                    break;
                case "POST":
                    result = ShowForm(request);
                    break;
                default:
                    break;
            }
            return result;
        }

        private string GetForm() =>
            "<form method=\"post\" action=\"form\">" +
            "<input type=\"text\" name=\"text1\" />" +
            "<input type=\"submit\" value=\"Submit\" />" +
            "</form>";

        private string WriteCookie(HttpResponse response)
        {
            response.Cookies.Append("color", "red", new CookieOptions { Path = "/cookies", Expires = DateTime.Now.AddDays(1) });
            return "<div>cookie written</div>";
        }

        private string ReadCookie(HttpRequest request)
        {
            var sb = new StringBuilder();
            IReadableStringCollection cookies = request.Cookies;
            foreach (var key in cookies.Keys)
            {
                sb.Append(GetDiv(key, cookies[key]));
            }
            return sb.ToString();
        }

        private string ShowForm(HttpRequest request)
        {
            var sb = new StringBuilder();
            if (request.HasFormContentType)
            {
                IFormCollection coll = request.Form;
                foreach (var key in coll.Keys)
                {
                    sb.Append(GetDiv(key, HtmlEncoder.Default.HtmlEncode(coll[key])));
                }
                return sb.ToString();
            }
            else return "no form";
        }


        public string Content(HttpRequest request)
        {

            string data = request.Query["data"];
            string encode = request.Query["encode"];
            if (string.Compare(encode, "true", true) == 0)
            {
                return HtmlEncoder.Default.HtmlEncode(data);
            }
            else
            {
                return data;
            }
        }

        public string Add(HttpRequest request)
        {
            var sb = new StringBuilder();
            StringValues xtext = request.Query["x"];
            StringValues ytext = request.Query["y"];
            if (xtext == null || ytext == null)
            {
                return "x and y must be set";
            }
            int x, y;
            if (!int.TryParse(xtext, out x))
            {
                return $"Error parsing {xtext}";
            }
            if (!int.TryParse(ytext, out y))
            {
                return $"Error parsing {ytext}";
            }
            return $"<div>{x} + {y} = {x + y}</div>";

        }

        private string GetDiv(string key, string value) =>
            $"<div><span>{key}:</span>&nbsp;<span>{value}</span></div>";

        public string GetRequestInformation(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(GetDiv("scheme", request.Scheme));
            sb.Append(GetDiv("host", request.Host.HasValue ? request.Host.Value : "no host"));
            sb.Append(GetDiv("path", request.Path));
            sb.Append(GetDiv("query string", request.QueryString.HasValue ? request.QueryString.Value : "no query string"));


            sb.Append(GetDiv("method", request.Method));
            sb.Append(GetDiv("protocol", request.Protocol));

            return sb.ToString();
        }

        public string GetHeaderInformation(HttpRequest request)
        {
            var sb = new StringBuilder();

            IHeaderDictionary headers = request.Headers;
            foreach (var header in request.Headers)
            {
                sb.Append(GetDiv(header.Key, string.Join("; ", header.Value)));
            }
            return sb.ToString();
        }

        public string GetTable(IDictionary<string, string[]> dict)
        {
            var sb = new StringBuilder();
            sb.Append("<table>");
            foreach (var key in dict.Keys)
            {
                sb.Append("<tr>");
                sb.Append($"<td>{key}:</td>");
                foreach (var value in dict[key])
                {
                    sb.Append($"<td>{value}</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
