using Microsoft.AspNetCore.Mvc;
using BookFinderAPI.Data;
using BookFinderAPI.Models;
using BookFinderAPI.Interfaces;

namespace BookFinderAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;

        public BookController(AppDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }
         
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string search = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var books = await _bookRepository.GetBookAsync(search);
            if (books == null)
            {
                return NotFound("Books not found.");
            }
            return Ok(books);
        }
        // Get Book by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = await _bookRepository.GetBookIdAsync(id); 
            if (book == null)
                return NotFound("Book not found.");

            return Ok(book);
        }
    }
}
