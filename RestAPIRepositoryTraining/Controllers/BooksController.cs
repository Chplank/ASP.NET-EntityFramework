using EntityFramework.Enities;
using EntityFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestAPIRepositoryTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;

        public BooksController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string title = null,
            [FromQuery] int? authorId = null,
            [FromQuery] int pageSize = 10)
        {
            if (pageSize <= 0)
            {
                return BadRequest("Page size must be greater than zero.");
            }

            var books = await _bookRepository.GetAllAsync();
            var filteredBooks = books
                .Where(b => (string.IsNullOrEmpty(title) || b.Title.Contains(title)) &&
                            (!authorId.HasValue || b.AuthorId == authorId))
                .Take(pageSize);

            if (!filteredBooks.Any())
            {
                return NotFound("No books found matching the criteria.");
            }

            return Ok(filteredBooks);
        }

        // GET: api/Books/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found.");
            }
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book object is null.");
            }

            try
            {
                var createdBook = await _bookRepository.AddAsync(book);
                return CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            if (book == null || id != book.Id)
            {
                return BadRequest("Invalid book data.");
            }

            try
            {
                await _bookRepository.UpdateAsync(book);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found.");
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found.");
            }
        }
    }
}
