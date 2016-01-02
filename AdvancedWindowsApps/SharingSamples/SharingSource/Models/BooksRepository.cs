using System.Collections.Generic;

namespace SharingSource.Models
{
    public class BooksRepository
    {
        public IEnumerable<Book> GetSampleBooks() =>
            new List<Book>()
            {
                new Book
                {
                    Title = "Professional C# 6 and .NET 5 Core",
                    Publisher = "Wrox Press"
                },
                new Book
                {
                    Title = "Professional C# 5.0 and .NET 4.5.1",
                    Publisher = "Wrox Press"
                }
            };

    }
}
