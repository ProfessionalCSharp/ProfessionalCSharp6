using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace MenusSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateDatabaseAsync().Wait();
        }

        private static async Task CreateDatabaseAsync()
        {
            using (var context = new MenusContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();

                string createdText = created ? "created" : "already exists";
                WriteLine($"database {createdText}");
            }
        }
    }
}
