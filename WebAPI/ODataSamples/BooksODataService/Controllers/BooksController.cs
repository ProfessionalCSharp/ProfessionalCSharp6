using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;
using Microsoft.AspNet.Mvc;
using BooksODataService.Models;

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

        // GET: api/values
        [HttpGet]
        public IEnumerable<Book> GetBooks() =>
            _booksContext.Books.Include(b => b.Chapters).ToList();


        // GET api/values/5
        [HttpGet("{id}")]
        public Book GetBook(int id) =>
            _booksContext.Books.SingleOrDefault(b => b.BookId == id);

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
