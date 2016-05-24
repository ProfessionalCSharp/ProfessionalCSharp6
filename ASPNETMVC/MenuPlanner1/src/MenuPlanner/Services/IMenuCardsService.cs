using MenuPlanner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MenuPlanner.Services
{
    public interface IMenuCardsService
    {
        Task AddMenuAsync(Menu menu);
        Task DeleteMenuAsync(int id);
        Task<Menu> GetMenuByIdAsync(int id);
        Task<IEnumerable<Menu>> GetMenusAsync();
        Task<IEnumerable<MenuCard>> GetMenuCardsAsync();
        Task UpdateMenuAsync(Menu menu);
    }

}
