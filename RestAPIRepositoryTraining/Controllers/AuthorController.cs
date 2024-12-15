using EntityFramework.Enities;
using EntityFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestAPIRepositoryTraining.Controllers
{
    /// <summary>
    /// Controller for managing authors through CRUD operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorController"/> class.
        /// </summary>
        /// <param name="authorRepository">The repository for performing operations on the Author entity.</param>
        public AuthorController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        /// <returns>A list of authors or a NotFound result if no authors exist.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authors = await _authorRepository.GetAllAsync();
            if (!authors.Any())
            {
                return NotFound("No authors found.");
            }

            return Ok(authors);
        }

        /// <summary>
        /// Retrieves an author by their unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>The author if found, or a NotFound result if the ID does not exist.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                return Ok(author);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Author with ID {id} not found.");
            }
        }

        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="author">The author entity to be created.</param>
        /// <returns>The created author or a BadRequest result if the input is invalid.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest("Author object is null.");
            }

            try
            {
                var createdAuthor = await _authorRepository.AddAsync(author);
                return Ok(createdAuthor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        /// <param name="id">The unique identifier of the author to update.</param>
        /// <param name="author">The updated author entity.</param>
        /// <returns>A NoContent result on success, or an error response if the input is invalid or the author is not found.</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Author author)
        {
            if (author == null || id != author.Id)
            {
                return BadRequest("Invalid author data.");
            }

            try
            {
                await _authorRepository.UpdateAsync(author);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Author with ID {id} not found.");
            }
        }

        /// <summary>
        /// Deletes an author by their unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the author to delete.</param>
        /// <returns>A NoContent result on success, or a NotFound result if the author is not found.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Author with ID {id} not found.");
            }
        }
    }
}
