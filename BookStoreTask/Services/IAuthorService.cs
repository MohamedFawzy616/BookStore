using BookStoreTask.DTOs;

namespace BookStoreTask.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorReadDto>> GetAllAuthorsAsync();
        Task<AuthorReadDto> GetAuthorByIdAsync(int id);
        Task<AuthorReadDto> CreateAuthorAsync(AuthorCreateDto authorDto);
        Task<bool> UpdateAuthorAsync(int id, AuthorUpdateDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
        Task<bool> AuthorExistsAsync(int id);
    }
}