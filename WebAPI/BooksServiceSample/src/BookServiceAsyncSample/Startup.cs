using BooksServiceSample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;

namespace BooksServiceAsyncSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.     
            services.AddMvc().AddXmlSerializerFormatters();

            services.AddDbContext<BooksContext>(options =>
                options.UseSqlServer(Configuration["Data:BookConnection:ConnectionString"]));

            services.AddSingleton<IBookChaptersRepository, BookChaptersRepository>();
            _container = services.BuildServiceProvider();
        }

        private IServiceProvider _container;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseMvc();

            EnsureDatabaseCreated();

        }

        private static bool s_created;

        private void EnsureDatabaseCreated()
        {
            if (!s_created)
            {
                using (var context = _container.GetService<BooksContext>())
                {
                    if (context.Database.EnsureCreated())
                    {
                        var c1 = new BookChapter { Number = 1, Title = "Application Architectures", Pages = 35 };
                        var c2 = new BookChapter { Number = 2, Title = "Core C#", Pages = 42 };
                        var c3 = new BookChapter { Number = 3, Title = "Objects and Types", Pages = 30 };
                        var c4 = new BookChapter { Number = 4, Title = "Inheritance", Pages = 18 };
                        var c5 = new BookChapter { Number = 5, Title = "Managed and Unmanaged Resources", Pages = 20 };
                        var c6 = new BookChapter { Number = 6, Title = "Generics", Pages = 22 };
                        var c7 = new BookChapter { Number = 38, Title = "Windows Store Apps", Pages = 45 };
                        var c8 = new BookChapter { Number = 41, Title = "ASP.NET Web Forms", Pages = 48 };
                        context.Chapters.AddRange(c1, c2, c3, c4, c5, c6, c7, c8);
                        context.SaveChanges();
                    }
                }
                s_created = true;
            }
        }
    }
}