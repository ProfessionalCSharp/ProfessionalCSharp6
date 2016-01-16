using DataBindingSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingSamples.Contracts
{
    public interface IBooksRepository
    {
        Task<Book> GetItemAsync(int id);
        Task<IEnumerable<Book>> GetItemsAsync();
        Task<Book> AddAsync(Book item);
        Task<Book> UpdateAsync(Book item);
        Task<bool> DeleteAsync(int id);
    }
}
