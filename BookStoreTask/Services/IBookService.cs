using BookStoreTask.DTOs;
using BookStoreTask.Models;

namespace BookStoreTask.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookReadDto>> GetAllBooksAsync();
        Task<BookReadDto> GetBookByIdAsync(int Id);
        Task<BookReadDto> CreateBookAsync(BookCreateDto book);
        Task<bool> UpdateBookAsync(int Id, BookUpdateDto book);
        Task<bool> DeleteBookAsync(int Id);
    }
}