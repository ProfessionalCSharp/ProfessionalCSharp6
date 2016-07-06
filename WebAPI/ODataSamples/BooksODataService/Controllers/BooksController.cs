using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using BooksODataService.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksODataService.Controllers
{
    [EnableQuery]
    [Route("odata/[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksContext _booksContext;
        public BooksController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks() =>
            _booksContext.Books.Include(b => b.Chapters).ToList();


        [HttpGet("{id}")]
        public Book GetBook(int id) =>
            _booksContext.Books.SingleOrDefault(b => b.BookId == id);

    }
}
