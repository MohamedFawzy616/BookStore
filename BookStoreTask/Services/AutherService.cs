using AutoMapper;
using BookStoreTask.Data.Repositories;
using BookStoreTask.DTOs;
using BookStoreTask.Models;

namespace BookStoreTask.Services
{
    public class AutherService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        public AutherService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public Task<bool> AuthorExistsAsync(int id)
        {
            return _authorRepository.AuthorExistsAsync(id);
        }

        public async Task<AuthorReadDto> CreateAuthorAsync(AuthorCreateDto authorDto)
        {
            Author author = _mapper.Map<Author>(authorDto);
            await _authorRepository.CreateAuthorAsync(author);
            return _mapper.Map<AuthorReadDto>(author);
        }

        public Task<bool> DeleteAuthorAsync(int id)
        {
            return _authorRepository.DeleteAuthorAsync(id);
        }

        public async Task<IEnumerable<AuthorReadDto>> GetAllAuthorsAsync()
        {
            IEnumerable<Author> authors = await _authorRepository.GetAllAuthorsAsync();
            return _mapper.Map<IEnumerable<AuthorReadDto>>(authors);
        }

        public async Task<AuthorReadDto> GetAuthorByIdAsync(int id)
        {
            Author author = await _authorRepository.GetAuthorByIdAsync(id);
            return _mapper.Map<AuthorReadDto>(author);
        }

        public async Task<bool> UpdateAuthorAsync(int id, AuthorUpdateDto authorDto)
        {
            Author author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author is null)
            {
                return false;
            }

            _mapper.Map(authorDto, author);
            await _authorRepository.UpdateAuthorAsync(author);
            return true;
        }
    }
}