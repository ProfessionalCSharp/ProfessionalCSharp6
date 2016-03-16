// RC2: using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BooksSample
{
    class Program
    {
        static void Main()
        {
            var p = new Program();
            p.InitializeServices();

            var service = p.Container.GetService<BooksService>();
            service.AddBooksAsync().Wait();
            service.ReadBooks();
        }

        private void InitializeServices()
        {
            const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=Books;trusted_connection=true";
           
            var services = new ServiceCollection();
            services.AddTransient<BooksService>();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BooksContext>(options =>
                    options.UseSqlServer(ConnectionString));
          

            Container = services.BuildServiceProvider();
        }


        public IServiceProvider Container { get; private set; }

      
    }
}
