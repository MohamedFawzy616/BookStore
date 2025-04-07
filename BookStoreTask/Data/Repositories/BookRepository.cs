using BookStoreTask.Models;
using BookStoreTask.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStoreTask.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;
        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int Id)
        {
            return await _context.Books.FindAsync(Id);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId)
        {
            throw new NotImplementedException();

        }
        public async Task<Book> GetBookByIdAndAuthorIdAsync(int id, int authorId)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id && b.AuthorId == authorId);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await SaveChangeAsync();
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await SaveChangeAsync();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            return await SaveChangeAsync();
        }

        public async Task<bool> BookExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
        public async Task<bool> BookExistsForAuthorAsync(int id, int authorId)
        {
            return await _context.Books.AnyAsync(b => b.Id == id && b.AuthorId == authorId);
        }
        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<IEnumerable<Book>> GetBooksByUserIdAsync(string userId)
        {
            return await _context.Books
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> IsBookOwnedByUserAsync(int bookId, string userId)
        {
            return await _context.Books
                .AnyAsync(b => b.Id == bookId && b.UserId == userId);
        }
    }
}