using BookStoreTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreTask.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookDbContext _context;
        public AuthorRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await SaveChangeAsync();
            return author;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            Author author = await GetAuthorByIdAsync(id);
            if (author == null)
            {
                return false;
            }

            _context.Authors.Remove(author);
            return await SaveChangeAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int Id)
        {
            return await _context.Authors.FindAsync(Id);
        }

        public async Task<Author> GetAuthorWithBooksAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            return await SaveChangeAsync();
        }
    }
}
