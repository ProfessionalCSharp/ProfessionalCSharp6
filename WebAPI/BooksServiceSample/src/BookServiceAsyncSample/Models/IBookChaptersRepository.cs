using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksServiceSample.Models
{
    public interface IBookChaptersRepository
    {
        Task InitAsync();
        Task AddAsync(BookChapter chapter);
        Task<BookChapter> RemoveAsync(Guid id);
        Task<IEnumerable<BookChapter>> GetAllAsync();
        Task<BookChapter> FindAsync(Guid id);
        Task UpdateAsync(BookChapter chapter);
    }
}