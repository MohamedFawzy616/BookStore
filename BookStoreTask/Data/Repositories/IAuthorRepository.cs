using BookStoreTask.Models;

namespace BookStoreTask.Data.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int Id);
        Task<Author> GetAuthorWithBooksAsync(int id);
        Task<Author> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);
        Task<bool> AuthorExistsAsync(int id);
        Task<bool> SaveChangeAsync();
    }
}