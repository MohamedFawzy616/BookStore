using BookStoreTask.DTOs;
using BookStoreTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoreTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Authors
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAuthors()
        {
            //_logger.LogInformation("Getting all authors");
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorReadDto>> GetAuthor(int id)
        {
            //_logger.LogInformation($"Getting author with ID: {id}");
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                //_logger.LogWarning($"Author with ID: {id} not found");
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorCreateDto>> CreateAuthor(AuthorCreateDto authorDto)
        {
            try
            {
                AuthorReadDto createdAuthor = await _authorService.CreateAuthorAsync(authorDto);
                return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error creating author");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto authorDto)
        {
            //_logger.LogInformation($"Updating author with ID: {id}");

            var exists = await _authorService.AuthorExistsAsync(id);
            if (!exists)
            {
                //_logger.LogWarning($"Author with ID: {id} not found");
                return NotFound();
            }

            await _authorService.UpdateAuthorAsync(id, authorDto);
            return NoContent();
        }

    }
}

/*
 POST /authors: Create a new author.
GET /authors: Retrieve a list of all authors.
GET /authors/:id: Retrieve a specific author by ID.
PUT /authors/:id: Update a specific author by ID.
DELETE /authors/:id: Delete a specific author by ID.
POST /authors/:authorId/books: Create a new book for a specific author.
GET /authors/:authorId/books: Retrieve all books for a specific author.
GET /authors/:authorId/books/:bookId: Retrieve a specific book by ID for a specific author.
PUT /authors/:authorId/books/:bookId: Update a specific book by ID for a specific author.
DELETE /authors/:authorId/books/:bookId: Delete a specific book by ID for a specific author.
 */