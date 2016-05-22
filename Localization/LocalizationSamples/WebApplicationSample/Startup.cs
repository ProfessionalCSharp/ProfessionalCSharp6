using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Net;

namespace WebApplicationSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "CustomResources");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IStringLocalizer<Startup> sr)
        {
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US")),
                SupportedCultures = new CultureInfo[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-AT"),
                    new CultureInfo("de")
                },
                SupportedUICultures = new CultureInfo[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-AT"),
                    new CultureInfo("de")
                }
            };

            app.UseRequestLocalization(options);

            app.Run(async (context) =>
            {
                IRequestCultureFeature requestCultureFeature =
                    context.Features.Get<IRequestCultureFeature>();
                RequestCulture requestCulture = requestCultureFeature.RequestCulture;

                var today = DateTime.Today;
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("<h1>Sample Localization</h1>");
                await context.Response.WriteAsync(
                $"<div>{requestCulture.Culture} {requestCulture.UICulture}</div>");
                await context.Response.WriteAsync($"<div>{today:D}</div>");
                await context.Response.WriteAsync($"<div>{sr["message1"]}</div>");
                await context.Response.WriteAsync($"<div>{sr.WithCulture(new CultureInfo("de-DE")).GetString("message1")}</div>");
                await context.Response.WriteAsync($"<div>{WebUtility.HtmlEncode(sr.GetString("message1"))}</div>");
                await context.Response.WriteAsync(
                  $"<div>{sr.GetString("message2", requestCulture.Culture, requestCulture.UICulture)}</div>");
            });
        }
    }
}
