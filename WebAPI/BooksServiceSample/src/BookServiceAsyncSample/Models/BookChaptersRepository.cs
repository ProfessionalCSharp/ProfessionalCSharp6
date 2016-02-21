using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksServiceSample.Models
{
    public class BookChaptersRepository : IBookChaptersRepository, IDisposable
    {
        private BooksContext _booksContext;

        public BookChaptersRepository(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public void Dispose()
        {
            _booksContext?.Dispose();
        }
        public async Task AddAsync(BookChapter chapter)
        {
            _booksContext.Chapters.Add(chapter);
            await _booksContext.SaveChangesAsync();
        }


        public Task<BookChapter> FindAsync(Guid id) =>
            _booksContext.Chapters.SingleOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<BookChapter>> GetAllAsync() =>
            await _booksContext.Chapters.ToListAsync();


        public Task InitAsync()
        {
            return Task.FromResult<object>(null);
        }

        public async Task<BookChapter> RemoveAsync(Guid id)
        {
            BookChapter chapter = await _booksContext.Chapters.SingleOrDefaultAsync(c => c.Id == id);
            if (chapter == null) return null;

            _booksContext.Chapters.Remove(chapter);
            await _booksContext.SaveChangesAsync();
            return chapter;
        }

        public async Task UpdateAsync(BookChapter chapter)
        {
            _booksContext.Chapters.Update(chapter);
            await _booksContext.SaveChangesAsync();
        }
    }
}
