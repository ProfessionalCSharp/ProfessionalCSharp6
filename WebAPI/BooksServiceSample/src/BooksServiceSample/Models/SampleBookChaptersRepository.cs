using BooksServiceSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace BooksServiceSample.Models
{
    public class SampleBookChaptersRepository : IBookChaptersRepository
    {
        private readonly ConcurrentDictionary<string, BookChapter> _chapters = new ConcurrentDictionary<string, BookChapter>();

        public void Init()
        {
            Add(new BookChapter { Number = 1, Title = "Application Architectures", Pages = 35 });
            Add(new BookChapter { Number = 2, Title = "Core C#", Pages = 42 });
            Add(new BookChapter { Number = 3, Title = "Objects and Types", Pages = 30 });
            Add(new BookChapter { Number = 4, Title = "Inheritance", Pages = 18 });
            Add(new BookChapter { Number = 5, Title = "Managed and Unmanaged Resources", Pages = 20 });
            Add(new BookChapter { Number = 6, Title = "Generics", Pages = 22 });
            Add(new BookChapter { Number = 38, Title = "Windows Store Apps", Pages = 45 });
            Add(new BookChapter { Number = 41, Title = "ASP.NET Web Forms", Pages = 48 });
        }


        public IEnumerable<BookChapter> GetAll() => _chapters.Values;

        public BookChapter Find(string id)
        {
            BookChapter chapter;
            _chapters.TryGetValue(id, out chapter);
            return chapter;
        }


        public void Add(BookChapter chapter)
        {
            chapter.Id = Guid.NewGuid().ToString();
            _chapters[chapter.Id] = chapter;
        }

        public void Update(BookChapter chapter)
        {
            _chapters[chapter.Id] = chapter;
        }

        public BookChapter Remove(string id)
        {
            BookChapter removed;
            _chapters.TryRemove(id, out removed);
            return removed;
        }

      
    }
}
