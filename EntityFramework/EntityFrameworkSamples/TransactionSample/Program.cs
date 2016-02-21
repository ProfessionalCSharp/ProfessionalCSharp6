using MenusSample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace TransactionSample
{
    class Program
    {
        static void Main()
        {
            PreparationAsync().Wait();
            AddTwoRecordsWithOneTxAsync().Wait();
            AddTwoRecordsWithTwoTxAsync().Wait();
            TwoSaveChangesWithOneTxAsync().Wait();

        }

        public static async Task PreparationAsync()
        {

            using (var context = new MenusContext())
            {
                bool deleted = await context.Database.EnsureDeletedAsync();
                string deletedText = deleted ? "deleted" : "does not exist";
                WriteLine($"database {deletedText}");

                bool created = await context.Database.EnsureCreatedAsync();

                string createdText = created ? "created" : "already exists";
                WriteLine($"database {createdText}");

                var card = new MenuCard() { Title = "Meat" };
                var m1 = new Menu { MenuCard = card, Text = "Wiener Schnitzel", Price = 12.90m };
                var m2 = new Menu { MenuCard = card, Text = "Goulash", Price = 8.80m };
                card.Menus.AddRange(new Menu[] { m1, m2 });
                context.MenuCards.Add(card);

                int records = await context.SaveChangesAsync();
                WriteLine($"{records} records added");
            }

        }

        private static async Task AddTwoRecordsWithOneTxAsync()
        {
            WriteLine(nameof(AddTwoRecordsWithOneTxAsync));
            try
            {
                using (var context = new MenusContext())
                {
                    var card = context.MenuCards.First();
                    var m1 = new Menu { MenuCardId = card.MenuCardId, Text = "added", Price = 99.99m };

                    int hightestCardId = await context.MenuCards.MaxAsync(c => c.MenuCardId);
                    var mInvalid = new Menu { MenuCardId = ++hightestCardId, Text = "invalid", Price = 999.99m };
                    context.Menus.AddRange(m1, mInvalid);

                    WriteLine("trying to add one invalid record to the database, this should fail...");
                    int records = await context.SaveChangesAsync();
                    WriteLine($"{records} records added");
                }
            }
            catch (DbUpdateException ex)
            {
                WriteLine($"{ex.Message}");
                WriteLine($"{ex?.InnerException.Message}");
            }
            WriteLine();
        }

        private static async Task AddTwoRecordsWithTwoTxAsync()
        {
            WriteLine(nameof(AddTwoRecordsWithTwoTxAsync));
            try
            {
                using (var context = new MenusContext())
                {
                    WriteLine("adding two records with two transactions to the database. One record should be written, the other not....");
                    var card = context.MenuCards.First();
                    var m1 = new Menu { MenuCardId = card.MenuCardId, Text = "added", Price = 99.99m };

                    context.Menus.Add(m1);
                    int records = await context.SaveChangesAsync();
                    WriteLine($"{records} records added");

                    int hightestCardId = await context.MenuCards.MaxAsync(c => c.MenuCardId);
                    var mInvalid = new Menu { MenuCardId = ++hightestCardId, Text = "invalid", Price = 999.99m };
                    context.Menus.Add(mInvalid);

                    records = await context.SaveChangesAsync();
                    WriteLine($"{records} records added");
                }
            }
            catch (DbUpdateException ex)
            {
                WriteLine($"{ex.Message}");
                WriteLine($"{ex?.InnerException.Message}");
            }
            WriteLine();
        }

        private static async Task TwoSaveChangesWithOneTxAsync()
        {
            WriteLine(nameof(TwoSaveChangesWithOneTxAsync));
            IDbContextTransaction tx = null;
            try
            {
                using (var context = new MenusContext())
                using (tx = await context.Database.BeginTransactionAsync())
                {

                    WriteLine("using one explicit transaction, writing should roll back...");
                    var card = context.MenuCards.First();
                    var m1 = new Menu { MenuCardId = card.MenuCardId, Text = "added with explicit tx", Price = 99.99m };

                    context.Menus.Add(m1);
                    int records = await context.SaveChangesAsync();
                    WriteLine($"{records} records added");


                    int hightestCardId = await context.MenuCards.MaxAsync(c => c.MenuCardId);
                    var mInvalid = new Menu { MenuCardId = ++hightestCardId, Text = "invalid", Price = 999.99m };
                    context.Menus.Add(mInvalid);

                    records = await context.SaveChangesAsync();
                    WriteLine($"{records} records added");

                    tx.Commit();
                }
            }
            catch (DbUpdateException ex)
            {
                WriteLine($"{ex.Message}");
                WriteLine($"{ex?.InnerException.Message}");

                WriteLine("rolling back...");
                tx.Rollback();
            }
            WriteLine();
        }

    }
}
