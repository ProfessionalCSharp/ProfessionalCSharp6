using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlanner.Models
{
    public class MenuCardDatabaseInitializer
    {
        private static bool _databaseChecked = false;

        public MenuCardDatabaseInitializer(MenuCardsContext context)
        {
            _context = context;
        }
        private MenuCardsContext _context;

        public async Task CreateAndSeedDatabaseAsync()
        {
            if (!_databaseChecked)
            {
                _databaseChecked = true;

                await _context.Database.MigrateAsync();

                if (_context.MenuCards.Count() == 0)
                {
                    var breakfast = new MenuCard { Name = "Breakfast", Active = true, Order = 1 };
                    var vegetarian = new MenuCard { Name = "Vegetarian", Active = true, Order = 2 };
                    var steaks = new MenuCard { Name = "Steaks", Active = true, Order = 3 };
                    _context.MenuCards.AddRange(breakfast, vegetarian, steaks);

                    var b1 = new Menu { Text = "Wiener Frühstück", MenuCard = breakfast, Active = true, Order = 1 };
                    var b2 = new Menu { Text = "Kantine's Frühstück", MenuCard = breakfast, Active = true, Order = 2 };
                    var b3 = new Menu { Text = "Frühstück für 2", MenuCard = breakfast, Active = true, Order = 3 };

                    _context.Menus.AddRange(b1, b2, b3);
                }

                await _context.SaveChangesAsync();
            }
        }
    }

}
