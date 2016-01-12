using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksServiceSample.Models
{
    public class SampleBookChaptersRepository : IBookChaptersRepository
    {
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters = new ConcurrentDictionary<Guid, BookChapter>();

        public async Task InitAsync()
        {
            await AddAsync(new BookChapter { Number = 1, Title = "Application Architectures", Pages = 35 });
            await AddAsync(new BookChapter { Number = 2, Title = "Core C#", Pages = 42 });
            await AddAsync(new BookChapter { Number = 3, Title = "Objects and Types", Pages = 30 });
            await AddAsync(new BookChapter { Number = 4, Title = "Inheritance", Pages = 18 });
            await AddAsync(new BookChapter { Number = 5, Title = "Managed and Unmanaged Resources", Pages = 20 });
            await AddAsync(new BookChapter { Number = 6, Title = "Generics", Pages = 22 });
            await AddAsync(new BookChapter { Number = 38, Title = "Windows Store Apps", Pages = 45 });
            await AddAsync(new BookChapter { Number = 41, Title = "ASP.NET Web Forms", Pages = 48 });
        }


        public Task AddAsync(BookChapter chapter)
        {
            chapter.Id = Guid.NewGuid();
            _chapters[chapter.Id] = chapter;
            return Task.FromResult<object>(null);
        }

        public Task<BookChapter> RemoveAsync(Guid id)
        {
            BookChapter removed;
            _chapters.TryRemove(id, out removed);
            return Task.FromResult(removed);
        }

        public Task<IEnumerable<BookChapter>> GetAllAsync() =>
            Task.FromResult<IEnumerable<BookChapter>>(_chapters.Values);


        public Task<BookChapter> FindAsync(Guid id)
        {
            BookChapter chapter;
            _chapters.TryGetValue(id, out chapter);
            return Task.FromResult(chapter);
        }

        public Task UpdateAsync(BookChapter chapter)
        {
            _chapters[chapter.Id] = chapter;
            return Task.FromResult<object>(null);
        }

    }
}
