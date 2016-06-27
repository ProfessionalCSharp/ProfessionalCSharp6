using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using WebSampleApp.Controllers;
using WebSampleApp.Middleware;
using WebSampleApp.Services;
using Microsoft.Extensions.Logging;
using System.Text;

namespace WebSampleApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISampleService, DefaultSampleService>();
            services.AddTransient<HomeController>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
                options.IdleTimeout = TimeSpan.FromMinutes(10));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddConsole();
            // loggerFactory.AddDebug();




            app.UseSession();
            // uncomment these lines to use the Middleware sample
            // app.UseHeaderMiddleware();
            // app.UseHeading1Middleware();


            app.Map("/home2", homeApp =>
            {
                homeApp.Run(async context =>
                {
                    HomeController controller =
                      app.ApplicationServices.GetService<HomeController>();
                    int statusCode = await controller.Index(context);
                    context.Response.StatusCode = statusCode;
                });
            });

            app.Map("/session", sessionApp =>
            {
                sessionApp.Run(async context =>
                {
                    await SessionSample.SessionAsync(context);
                });
            });

            PathString remaining;
            app.MapWhen(context => context.Request.Path.StartsWithSegments("/configuration", out remaining),
                configApp =>
                {
                    configApp.Run(async context =>
                    {
                        if (remaining.StartsWithSegments("/appsettings"))
                        {
                            await ConfigSample.AppSettings(context, Configuration);
                        }
                        else if (remaining.StartsWithSegments("/database"))
                        {
                            await ConfigSample.ReadDatabaseConnection(context, Configuration);
                        }
                        else if (remaining.StartsWithSegments("/secret"))
                        {
                            await ConfigSample.UserSecret(context, Configuration);
                        }
                    });
                });


            app.Map("/configuration", configApp =>
            {
                configApp.Run(async context =>
                {
                    await ConfigSample.ReadDatabaseConnection(context, Configuration);
                });
            });

            //// uncomment this app.Run invocation to active the Hello, World! output
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //// uncomment this app.Run invocation for the first Request and Response sample
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(RequestAndResponseSample.GetRequestInformation(context.Request));
            //});


            //// uncomment this app.Run invocation for the custom routing
            //app.Run(async (context) =>
            //{
            //    if (context.Request.Path.Value.ToLower() == "/home")
            //    {
            //        HomeController controller =
            //          app.ApplicationServices.GetService<HomeController>();
            //        int statusCode = await controller.Index(context);
            //        context.Response.StatusCode = statusCode;
            //        return;
            //    }
            //}

            //// uncomment this app.Run invocation for request/response samples
            //app.Run(async (context) =>
            //{
            //    string result = string.Empty;
            //    switch (context.Request.Path.Value.ToLower())
            //    {
            //        case "/header":
            //            result = RequestAndResponseSample.GetHeaderInformation(context.Request);
            //            break;
            //        case "/add":
            //            result = RequestAndResponseSample.QueryString(context.Request);
            //            break;
            //        case "/content":
            //            result = RequestAndResponseSample.Content(context.Request);
            //            break;
            //        case "/encoded":
            //            result = RequestAndResponseSample.ContentEncoded(context.Request);
            //            break;
            //        case "/form":
            //            result = RequestAndResponseSample.GetForm(context.Request);
            //            break;
            //        case "/writecookie":
            //            result = RequestAndResponseSample.WriteCookie(context.Response);
            //            break;
            //        case "/readcookie":
            //            result = RequestAndResponseSample.ReadCookie(context.Request);
            //            break;
            //        case "/json":
            //            result = RequestAndResponseSample.GetJson(context.Response);
            //            break;
            //        default:
            //            result = RequestAndResponseSample.GetRequestInformation(context.Request);
            //            break;
            //    }
            //    await context.Response.WriteAsync(result);
            //});

            //// uncomment to use the home controller
            //app.Run(async (context) =>
            //{
            //    if (context.Request.Path.Value.ToLower() == "/home")
            //    {
            //        HomeController controller =
            //          app.ApplicationServices.GetService<HomeController>();
            //        int statusCode = await controller.Index(context);
            //        context.Response.StatusCode = statusCode;
            //        return;
            //    }
            //});

            app.Map("/RequestAndResponse", app1 =>
            {
                app1.Run(async (context) =>
                {
                    await context.Response.WriteAsync(
                        RequestAndResponseSample.GetRequestInformation(context.Request));
                });
            });

            app.Map("/RequestAndResponse2", app1 =>
            {
                app1.Run(async (context) =>
                {
                    string result = string.Empty;
                    switch (context.Request.Path.Value.ToLower())
                    {
                        case "/header":
                            result = RequestAndResponseSample.GetHeaderInformation(context.Request);
                            break;
                        case "/add":
                            result = RequestAndResponseSample.QueryString(context.Request);
                            break;
                        case "/content":
                            result = RequestAndResponseSample.Content(context.Request);
                            break;
                        case "/encoded":
                            result = RequestAndResponseSample.ContentEncoded(context.Request);
                            break;
                        case "/form":
                            result = RequestAndResponseSample.GetForm(context.Request);
                            break;
                        case "/writecookie":
                            result = RequestAndResponseSample.WriteCookie(context.Response);
                            break;
                        case "/readcookie":
                            result = RequestAndResponseSample.ReadCookie(context.Request);
                            break;
                        case "/json":
                            result = RequestAndResponseSample.GetJson(context.Response);
                            break;
                        default:
                            result = RequestAndResponseSample.GetRequestInformation(context.Request);
                            break;
                    }
                    await context.Response.WriteAsync(result);
                });
            });

            // contrary to the sample as it is written in the book I've made it now easier for you
            // to start all the different parts by adding this list
            // This required minor changes to the code samples. However, you can comment the following code block 
            // and uncomment the specific parts as described in the book
            app.Run(async (context) =>
            {
                var sb = new StringBuilder();
                sb.Append("<ul>");
                sb.Append(@"<li><a href=""/hello.html"">Static Files</a> - requires UseStaticFiles</li>");
                sb.Append(@"<li><a href=""/RequestAndResponse"">Request and Response</a>");
                sb.Append("<li>Request and Response 2");
                sb.Append("<ul>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/header"">Header</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/add?x=3&y=4"">Add</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/content?data=sample"">Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/content?data=<h1>sample</h1>"">HTML Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/content?data=<script>alert('hacker');</script>"">Bad Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/encoded?data=<h1>sample</h1>"">Encoded content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/encoded?data=<script>alert('hacker');</script>"">Encoded bad Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/form"">Form</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/writecookie"">Write cookie</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/readcookie"">Read cookie</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/json"">JSON</a></li>");
                sb.Append("</ul>");
                sb.Append("</li>");
                sb.Append(@"<li><a href=""/home2"">Home Controller with dependency injection</a></li>");
                sb.Append(@"<li><a href=""/session"">Session</a></li>");
                sb.Append("<li>Configuration");
                sb.Append("<ul>");
                sb.Append(@"<li><a href=""/configuration/appsettings"">Appsettings</a></li>");
                sb.Append(@"<li><a href=""/configuration/database"">Database</a></li>");
                sb.Append(@"<li><a href=""/configuration/secret"">Secrets  </a></li>");
                sb.Append("</ul>");
                sb.Append("</li>");
                sb.Append("</ul>");
                await context.Response.WriteAsync(sb.ToString());
            });

        }
    }
}
