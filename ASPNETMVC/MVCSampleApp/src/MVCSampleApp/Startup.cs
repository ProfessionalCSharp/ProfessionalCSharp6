using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVCSampleApp.Models;
using MVCSampleApp.Services;


namespace MVCSampleApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddMvc();
            services.AddTransient<ISampleService, DefaultSampleService>();
            services.AddScoped<EventsAndMenusContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }

            app.UseIISPlatformHandler();
            app.UseStaticFiles();


            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" }
              ).MapRoute(
                name: "language",
                template: "{language}/{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" }
            ));

            app.UseMvc(routes =>
              routes.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" })
              .MapRoute(
                name: "language",
                template: "{language}/{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { language = @"(en)|(de)" })
              .MapRoute(
                name: "multipleparameters",
                template: "{controller}/{action}/{x}/{y}",
                defaults: new { controller = "Home", action = "Add" },
                constraints: new { x = @"\d", y = @"\d" }
                ));

            app.UseMvc(routes => routes.MapRoute(
                name: "language",
                template: "{language}/{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { language = @"(en)|(de)" }
            ));

            app.UseMvcWithDefaultRoute();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
