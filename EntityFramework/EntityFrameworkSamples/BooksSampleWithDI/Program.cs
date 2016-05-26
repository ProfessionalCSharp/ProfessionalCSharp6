using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Console;

namespace BooksSample
{
    class Program
    {
        static void Main()
        {
            try
            {
                var p = new Program();
                p.InitializeServices();

                var service = p.Container.GetService<BooksService>();
                service.AddBooksAsync().Wait();
                service.ReadBooks();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private void InitializeServices()
        {
            const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=Books;trusted_connection=true";
           
            var services = new ServiceCollection();
            services.AddTransient<BooksService>();
           
            services.AddDbContext<BooksContext>(options =>
                options.UseSqlServer(ConnectionString));
          

            Container = services.BuildServiceProvider();
        }


        public IServiceProvider Container { get; private set; }

      
    }
}
