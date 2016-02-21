using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace MenusSample
{
    class Program
    {
        static void Main()
        {
            CreateDatabaseAsync().Wait();
            AddRecordsAsync().Wait();
            ObjectTracking();
            UpdateRecordsAsync().Wait();
            ChangeUntrackedAsync().Wait();
        }

        private static async Task ChangeUntrackedAsync()
        {
            WriteLine(nameof(ChangeUntrackedAsync));
            Menu m = await GetMenuAsync();
            m.Price += 0.7m;
            await UpdateUntrackedAsync(m);
            WriteLine();
        }

        private static async Task UpdateUntrackedAsync(Menu m)
        {
            using (var context = new MenusContext())
            {
                ShowState(context);

                //EntityEntry<Menu> entry = context.Menus.Attach(m);
                //entry.State = EntityState.Modified;
               
                context.Menus.Update(m);
                ShowState(context);

                await context.SaveChangesAsync();

            }
        }

        private static async Task<Menu> GetMenuAsync()
        {
            using (var context = new MenusContext())
            {
                Menu menu = await context.Menus
                                    .Skip(2)
                                    .FirstOrDefaultAsync();
                return menu;
            }
        }

        private static async Task UpdateRecordsAsync()
        {
            WriteLine(nameof(UpdateRecordsAsync));
            using (var context = new MenusContext())
            {
                Menu menu = await context.Menus
                                    .Skip(1)
                                    .FirstOrDefaultAsync();

                
                ShowState(context);
                menu.Price += 0.2m;
                ShowState(context);

                int records = await context.SaveChangesAsync();
                WriteLine($"{records} updated");
                ShowState(context);                    
            }
            WriteLine();
        }
        private static void ObjectTracking()
        {
            WriteLine(nameof(ObjectTracking));
            using (var context = new MenusContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var m1 = (from m in context.Menus.AsNoTracking()
                          where m.Text.StartsWith("Con")
                          select m).FirstOrDefault();

                var m2 = (from m in context.Menus
                          where m.Text.Contains("(")
                          select m).FirstOrDefault();

                if (object.ReferenceEquals(m1, m2))
                {
                    WriteLine("the same object");
                }
                else
                {
                    WriteLine("not the same");
                }

                ShowState(context);
            }
            WriteLine();
        }

        private static async Task CreateDatabaseAsync()
        {
            using (var context = new MenusContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();

                string createdText = created ? "created" : "already exists";
                WriteLine($"database {createdText}");

                // await context.Database.MigrateAsync();
            }
        }


        private static async Task AddRecordsAsync()
        {
            WriteLine(nameof(AddRecordsAsync));
            try
            {
                using (var context = new MenusContext())
                {
                    
                    var soupCard = new MenuCard();
                    Menu[] soups =
                    {
                        new Menu { Text = "Consommé Célestine (with shredded pancake)", Price = 4.8m, MenuCard =soupCard},
                        new Menu { Text = "Baked Potato Soup", Price = 4.8m, MenuCard = soupCard },
                        new Menu { Text = "Cheddar Broccoli Soup", Price = 4.8m, MenuCard = soupCard },
                    };

                    soupCard.Title = "Soups";
                    soupCard.Menus.AddRange(soups);

                    context.MenuCards.Add(soupCard);

                    ShowState(context);

                    int records = await context.SaveChangesAsync();
                    WriteLine($"{records} added");
                    WriteLine();
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            WriteLine();
        }

        public static void ShowState(MenusContext context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                WriteLine($"type: {entry.Entity.GetType().Name}, state: {entry.State}, {entry.Entity}");
            }
            WriteLine();
        }
    }
}
