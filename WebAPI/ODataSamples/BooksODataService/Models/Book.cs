using System.Collections.Generic;

namespace BooksODataService.Models
{
    public class Book
    {
        public Book()
        {
            Chapters = new List<Chapter>();
        }
        public int BookId { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; }
    }
}
