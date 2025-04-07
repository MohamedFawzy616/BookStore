using BookStoreTask.Models;
namespace BookStoreTask.Data.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int Id);
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId);
        Task<Book> GetBookByIdAndAuthorIdAsync(int id, int authorId);
        Task<Book>CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<bool> BookExistsAsync(int id);
        Task<bool> BookExistsForAuthorAsync(int id, int authorId);
        Task<bool> SaveChangeAsync();

        Task<IEnumerable<Book>> GetBooksByUserIdAsync(string userId);
        Task<bool> IsBookOwnedByUserAsync(int bookId, string userId);
    }
}