using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;
using BooksServiceSample.Models;

namespace BookServiceAsyncSample
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
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddXmlSerializerFormatters();
            // uncomment the following three lines to use the SampleBookChaptersRepository
            IBookChaptersRepository repos = new SampleBookChaptersRepository();
            // RC2 services.AddSingleton<IBookChaptersRepository>(repos);
            services.AddInstance<IBookChaptersRepository>(repos);
            await repos.InitAsync();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BooksContext>(options =>
                    options.UseSqlServer(Configuration["Data:BookConnection:ConnectionString"])
            );

            services.AddSingleton<IBookChaptersRepository, BookChaptersRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
