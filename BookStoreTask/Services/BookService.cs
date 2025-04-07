using AutoMapper;
using BookStoreTask.Data.Repositories;
using BookStoreTask.DTOs;
using BookStoreTask.Models;

namespace BookStoreTask.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper mapper;
        private readonly IBookRepository repository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository _bookRepository, IMapper _mapper, ILogger<BookService> logger)
        {
            repository = _bookRepository;
            this.mapper = _mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
        {
            //_logger.LogInformation("Getting all books for user: {UserId}", userId);
            var books = await repository.GetAllBooksAsync();
            return mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<BookReadDto> GetBookByIdAsync(int Id)
        {
            var book = await repository.GetBookByIdAsync(Id);
            if (book == null)
                return null;

            return mapper.Map<BookReadDto>(book);
        }

        public async Task<BookReadDto> CreateBookAsync(BookCreateDto bookDto)
        {
            var Model = mapper.Map<Book>(bookDto);
            await repository.CreateBookAsync(Model);
            return mapper.Map<BookReadDto>(Model);
        }

        public async Task<bool> DeleteBookAsync(int Id)
        {
            return await repository.DeleteBookAsync(Id);
        }

        public async Task<bool> UpdateBookAsync(int Id, BookUpdateDto book)
        {
            var bookModel = await repository.GetBookByIdAsync(Id);
            if (bookModel == null)
                return false;

            mapper.Map(book, bookModel);
            await repository.UpdateBookAsync(bookModel);
            return true;
        }

    }
}