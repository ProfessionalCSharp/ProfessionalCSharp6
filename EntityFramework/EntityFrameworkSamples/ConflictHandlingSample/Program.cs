using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BooksSample
{

    static class ByteArrayExtension
    {
        public static string StringOutput(this byte[] data)
        {
            var sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append($"{b}.");
            }
            return sb.ToString();
        }
    }

    class Program
    {
        private const string BookTitle = "sample book";
        static void Main(string[] args)
        {
            CreateDatabaseAsync().Wait();
            AddBookAsync().Wait();
            ConflictHandlingAsync().Wait();
        }

        private static async Task CreateDatabaseAsync()
        {
            using (var context = new BooksContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();

                string createdText = created ? "created" : "already exists";
                WriteLine($"database {createdText}");
            }
        }

        private static async Task AddBookAsync()
        {
            using (var context = new BooksContext())
            {
                var b = new Book { Title = BookTitle, Publisher = "Sample" };

                context.Add(b);
                int records = await context.SaveChangesAsync();

                WriteLine($"{records} record added");
            }
            WriteLine();
        }

        public static async Task ConflictHandlingAsync()
        {
            // user 1
            Tuple<BooksContext, Book> tuple1 = await PrepareUpdateAsync();
            tuple1.Item2.Title = "user 1 wins";

            // user 2
            Tuple<BooksContext, Book> tuple2 = await PrepareUpdateAsync();
            tuple2.Item2.Title = "user 2 wins";

            await UpdateAsync(tuple1.Item1, tuple1.Item2, "user 1");
            await UpdateAsync(tuple2.Item1, tuple2.Item2, "user 2");

            tuple1.Item1.Dispose();
            tuple2.Item1.Dispose();

            await CheckUpdateAsync(tuple1.Item2.BookId);
               
        }

        private static async Task<Tuple<BooksContext, Book>> PrepareUpdateAsync()
        {
            var context = new BooksContext();
            Book book = await context.Books.Where(b => b.Title == BookTitle).FirstOrDefaultAsync();
            return Tuple.Create(context, book);
        }

        private static async Task UpdateAsync(BooksContext context, Book book, string user)
        {
            try
            {
                WriteLine($"{user}: updating id {book.BookId}, timestamp {book.TimeStamp.StringOutput()}");
                ShowChanges(book.BookId, context.Entry(book));

                int records = await context.SaveChangesAsync();
                WriteLine($"{user}: updated {book.TimeStamp.StringOutput()}");
                WriteLine($"{user}: {records} record(s) updated while updating {book.Title}");
            }
            catch (DbUpdateConcurrencyException ex)
            { 
                WriteLine($"{user} update failed with {book.Title}");
                WriteLine($"{user} error: {ex.Message}");
                foreach (var entry in ex.Entries)
                {
                    Book b = entry.Entity as Book;

                    WriteLine($"{b.Title} {b.TimeStamp.StringOutput()}");
                    ShowChanges(book.BookId, context.Entry(book));
                }
            }            
        }

        private static void ShowChanges(int id, EntityEntry entity)
        {
            ShowChange(id, entity.Property("Title"));
            ShowChange(id, entity.Property("Publisher"));
        }

        private static void ShowChange(int id, PropertyEntry propertyEntry)
        {
            WriteLine($"id: {id}, current: {propertyEntry.CurrentValue}, original: {propertyEntry.OriginalValue}, modified: {propertyEntry.IsModified}");
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
