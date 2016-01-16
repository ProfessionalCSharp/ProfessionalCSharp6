using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksODataService.Models
{
    public class Book
    {
        public Book()
        {
            Chpaters = new List<Chapter>();
        }
        public int BookId { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; }
    }
}
