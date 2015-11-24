using System.Threading.Tasks;
using static System.Console;

namespace BooksSample
{
    public class BooksService
    {
        private readonly BooksContext _booksContext;
        public BooksService(BooksContext context)
        {
            _booksContext = context;
        }

        public async Task AddBooksAsync()
        {
            var b1 = new Book { Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press" };
            var b2 = new Book { Title = "Professional C# 2012 and .NET 4.5", Publisher = "Wrox Press" };
            var b3 = new Book { Title = "JavaScript for Kids", Publisher = "Wrox Press" };
            var b4 = new Book { Title = "Web Design with HTML and CSS", Publisher = "For Dummies" };
            _booksContext.AddRange(b1, b2, b3, b4);
            int records = await _booksContext.SaveChangesAsync();

            WriteLine($"{records} records added");
        }

        public void ReadBooks()
        {

            var books = _booksContext.Books;
            foreach (var b in books)
            {
                WriteLine($"{b.Title} {b.Publisher}");
            }
            WriteLine();
        }

    }
}
