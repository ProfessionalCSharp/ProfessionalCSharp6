using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace BooksSample
{
    class Program
    {
        static void Main()
        {
            var p = new Program();
            p.AddBookAsync("Professional C# 6", "Wrox Press").Wait();
            p.AddBooksAsync().Wait();
            p.ReadBooks();
            p.QueryBooks();
            p.UpdateBookAsync().Wait();

            ConflictHandlingAsync().Wait();

            p.DeleteBooksAsync().Wait();

        }

        private async Task AddBookAsync(string title, string publisher)
        {
            using (var context = new BooksContext())
            {
                var book = new Book { Title = title, Publisher = publisher };
                context.Add(book);
                int records = await context.SaveChangesAsync();

                WriteLine($"{records} record added");
            }
            WriteLine();
        }

        private async Task AddBooksAsync()
        {
            using (var context = new BooksContext())
            {
                var b1 = new Book { Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press" };
                var b2 = new Book { Title = "Professional C# 2012 and .NET 4.5", Publisher = "Wrox Press" };
                var b3 = new Book { Title = "JavaScript for Kids", Publisher = "Wrox Press" };
                var b4 = new Book { Title = "Web Design with HTML and CSS", Publisher = "For Dummies" };
                var b5 = new Book { Title = "Conflict Handling", Publisher = "Test" };
                context.AddRange(b1, b2, b3, b4, b5);
                int records = await context.SaveChangesAsync();

                WriteLine($"{records} records added");
            }
            WriteLine();
        }

        private void ReadBooks()
        {
            using (var context = new BooksContext())
            {
                var books = context.Books;
                foreach (var b in books)
                {
                    WriteLine($"{b.Title} {b.Publisher}");
                }
            }
            WriteLine();
        }

        private void QueryBooks()
        {
            using (var context = new BooksContext())
            {
                var wroxBooks = context.Books.Where(b => b.Publisher == "Wrox Press");

                // comment the previous line and uncomment the next lines to try the LINQ query syntax
                //var wroxBooks = from b in context.Books
                //                where b.Publisher == "Wrox Press"
                //                select b;
                foreach (var b in wroxBooks)
                {
                    WriteLine($"{b.Title} {b.Publisher}");
                }
            }
            WriteLine();
        }

        private async Task UpdateBookAsync()
        {
            using (var context = new BooksContext())
            {
                int records = 0;
                var book = context.Books.Where(b => b.Title == "Professional C# 6").FirstOrDefault();
                if (book != null)
                {
                    book.Title = "Professional C# 6 and .NET Core 5";
                    records = await context.SaveChangesAsync();
                }

                WriteLine($"{records} record updated");
            }
            WriteLine();
        }

        private async Task DeleteBooksAsync()
        {
            using (var context = new BooksContext())
            {
                var books = context.Books;
                context.Books.RemoveRange(books);
                int records = await context.SaveChangesAsync();
                WriteLine($"{records} records deleted");
            }
            WriteLine();
        }

        public static async Task ConflictHandlingAsync()
        {
            // user 1
            Tuple<BooksContext, Book> tuple1 = await PrepareUpdateAsync();
            tuple1.Item2.Title = "updated from user 1";

            // user 2
            Tuple<BooksContext, Book> tuple2 = await PrepareUpdateAsync();
            tuple2.Item2.Title = "updated from user 2";

            // user 1
            await UpdateAsync(tuple1.Item1, tuple1.Item2);
            // user 2
            await UpdateAsync(tuple2.Item1, tuple2.Item2);

            tuple1.Item1.Dispose();
            tuple2.Item1.Dispose();

            await CheckUpdateAsync(tuple1.Item2.BookId);

        }

        private static async Task<Tuple<BooksContext, Book>> PrepareUpdateAsync()
        {
            var context = new BooksContext();
            Book book = await context.Books.Where(b => b.Title == "Conflict Handling").FirstOrDefaultAsync();
            return Tuple.Create(context, book);
        }

        private static async Task UpdateAsync(BooksContext context, Book book)
        {

            await context.SaveChangesAsync();
            WriteLine($"successfully written to the database: id {book.BookId} with title {book.Title}");
        }

        private static async Task CheckUpdateAsync(int id)
        {
            using (var context = new BooksContext())
            {
                Book book = await context.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
                WriteLine($"updated: {book.Title}");
            }
        }
    }
}
