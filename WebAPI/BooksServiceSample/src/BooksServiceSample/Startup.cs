using BooksServiceSample.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Mvc.Formatters;
using Swashbuckle.SwaggerGen;
using Swashbuckle.SwaggerGen.XmlComments;

namespace BooksServiceSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.        
            services.AddMvc().AddXmlSerializerFormatters();

            IBookChaptersRepository repos = new SampleBookChaptersRepository();
            repos.Init();
            // RC2 services.AddSingleton<IBookChaptersRepository>(repos);
            services.AddInstance<IBookChaptersRepository>(repos);

            services.AddSwaggerGen();

            services.AddSwaggerGen();
            services.ConfigureSwaggerDocument(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Book Chapters",
                    Description = "A sample for Professional C# 6"
                });
                options.IgnoreObsoleteActions = true;
            });

            services.ConfigureSwaggerSchema(options =>
            {
                options.DescribeAllEnumsAsStrings = true;
                options.IgnoreObsoleteProperties = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();

            app.UseSwaggerGen();
            app.UseSwaggerUi();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
