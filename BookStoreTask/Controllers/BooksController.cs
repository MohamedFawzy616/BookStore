using BookStoreTask.DTOs;
using BookStoreTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Gets all books
        /// </summary>
        /// <returns>A list of books</returns>
        /// <response code="200">Returns the list of books</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Gets a book by id
        /// </summary>
        /// <param name="id">The book ID</param>
        /// <returns>A book</returns>
        /// <response code="200">Returns the book</response>
        /// <response code="404">If the book doesn't exist</response>
        [HttpGet("{id}", Name = "GetBookById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookReadDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with ID {id} not found" });

            return Ok(book);
        }

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="bookDto">The book data</param>
        /// <returns>The created book</returns>
        /// <response code="201">Returns the newly created book</response>
        /// <response code="400">If the book data is invalid</response>
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookReadDto>> CreateBook([FromBody] BookCreateDto bookDto)
        {
            var createdBook = await _bookService.CreateBookAsync(bookDto);
            return CreatedAtRoute(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        /// <summary>
        /// Updates a book
        /// </summary>
        /// <param name="id">The book ID</param>
        /// <param name="bookDto">The updated book data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the book was updated successfully</response>
        /// <response code="400">If the book data is invalid</response>
        /// <response code="404">If the book doesn't exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookUpdateDto bookDto)
        {
            var result = await _bookService.UpdateBookAsync(id, bookDto);
            if (!result)
                return NotFound(new { message = $"Book with ID {id} not found" });

            return NoContent();
        }

        /// <summary>
        /// Deletes a book
        /// </summary>
        /// <param name="id">The book ID</param>
        /// <returns>No content</returns>
        /// <response code="204">If the book was deleted successfully</response>
        /// <response code="404">If the book doesn't exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                return NotFound(new { message = $"Book with ID {id} not found" });

            return NoContent();
        }
    }
}