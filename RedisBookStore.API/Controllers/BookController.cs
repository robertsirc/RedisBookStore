using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisBookStore.API.Models;
using RedisBookStore.API.Services;

namespace RedisBookStore.API.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }
        
        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var results = await _bookService.GetBook(id);
            return Ok(results);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            var results = await _bookService.CreateBook(book);
            return CreatedAtAction(nameof(Create), results.Id, results);
        }

        [HttpPost]
        [Route("load")]
        public async Task<ActionResult<Book>>  Load(Book[] books)
        {
            var results = await _bookService.CreateBooks(books);
            return  CreatedAtAction(nameof(Load), null, results);
        }
    }
}