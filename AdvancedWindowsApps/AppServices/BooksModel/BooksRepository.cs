using System.Collections.Generic;

namespace BooksModel
{
    public class BooksRepository
    {
        private readonly List<Book> _books = new List<Book>()
        {
            new Book {Title = "Professional C# 6", Publisher = "Wrox Press" }
        };
        public IEnumerable<Book> Books => _books;

        public void AddBook(Book book)
        {
            _books.Add(book);
        }
    }
}
