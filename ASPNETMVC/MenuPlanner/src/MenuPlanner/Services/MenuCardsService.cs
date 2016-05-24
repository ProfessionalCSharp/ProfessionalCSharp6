using MenuPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlanner.Services
{
    public class MenuCardsService : IMenuCardsService
    {
        private readonly MenuCardsContext _menuCardsContext;
        public MenuCardsService(MenuCardsContext menuCardsContext)
        {
            _menuCardsContext = menuCardsContext;
        }

        public async Task<IEnumerable<Menu>> GetMenusAsync()
        {
            await EnsureDatabaseCreated();

            var menus = _menuCardsContext.Menus.Include(m => m.MenuCard);
            return await menus.ToArrayAsync();
        }

        public async Task<IEnumerable<MenuCard>> GetMenuCardsAsync()
        {
            await EnsureDatabaseCreated();

            var menuCards = _menuCardsContext.MenuCards;
            return await menuCards.ToArrayAsync();
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            return await _menuCardsContext.Menus.SingleOrDefaultAsync(
              m => m.Id == id);
        }

        public async Task AddMenuAsync(Menu menu)
        {
            _menuCardsContext.Menus.Add(menu);
            await _menuCardsContext.SaveChangesAsync();
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _menuCardsContext.Entry(menu).State = EntityState.Modified;
            await _menuCardsContext.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(int id)
        {
            Menu menu = _menuCardsContext.Menus.Single(m => m.Id == id);

            _menuCardsContext.Menus.Remove(menu);
            await _menuCardsContext.SaveChangesAsync();
        }

        private async Task EnsureDatabaseCreated()
        {
            var init = new MenuCardDatabaseInitializer(_menuCardsContext);
            await init.CreateAndSeedDatabaseAsync();
        }
    }

}
