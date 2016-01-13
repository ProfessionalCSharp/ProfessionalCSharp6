using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
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
                    _context.MenuCards.Add(
                      new MenuCard { Name = "Breakfast", Active = true, Order = 1 });
                    _context.MenuCards.Add(
                      new MenuCard { Name = "Vegetarian", Active = true, Order = 2 });
                    _context.MenuCards.Add(
                      new MenuCard { Name = "Steaks", Active = true, Order = 3 });
                }

                await _context.SaveChangesAsync();
            }
        }
    }

}
