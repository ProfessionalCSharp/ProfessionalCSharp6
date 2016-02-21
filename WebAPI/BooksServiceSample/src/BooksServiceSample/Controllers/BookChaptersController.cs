using BooksServiceSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksServiceSample.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    public class BookChaptersController : Controller
    {
        private readonly IBookChaptersRepository _repository;
        public BookChaptersController(IBookChaptersRepository repository)
        {
            _repository = repository;
        }

        // GET: api/bookchapters
        [HttpGet()]
        public IEnumerable<BookChapter> GetBookChapters() => _repository.GetAll();

        // GET api/bookchapters/guid
        [HttpGet("{id}", Name = nameof(GetBookChapterById))]
        public IActionResult GetBookChapterById(string id)
        {
            BookChapter chapter = _repository.Find(id);
            if (chapter == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(chapter);
            }
        }

        // POST api/bookchapters
        [HttpPost]
        public IActionResult PostBookChapter([FromBody]BookChapter chapter)
        {
            if (chapter == null)
            {
                return BadRequest();
            }
            _repository.Add(chapter);
            // return a 201 response, Created
            return CreatedAtRoute(nameof(GetBookChapterById), new { id = chapter.Id }, chapter);
        }

        // PUT api/bookchapters/guid
        [HttpPut("{id}")]
        public IActionResult PutBookChapter(string id, [FromBody]BookChapter chapter)
        {
            if (chapter == null || id != chapter.Id)
            {
                return BadRequest();
            }
            if (_repository.Find(id) == null)
            {
                return NotFound();
            }

            _repository.Update(chapter);
            return new NoContentResult();  // 204
        }

        // DELETE api/bookchapters/guid
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repository.Remove(id);
            // void returns 204, No Content
        }
    }
}
