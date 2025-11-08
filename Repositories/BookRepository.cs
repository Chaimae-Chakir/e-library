using Jadev.Library.Managment.Data;
using Jadev.Library.Managment.Dtos;
using Jadev.Library.Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Jadev.Library.Managment.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) => _context = context;
        public async Task<Book> Add(Book book)
        {
           _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task DeleteById(int id)
        {
            var book = _context.Books.FindAsync(id);
            if(book != null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Book>> GetAll() => await _context.Books.ToListAsync();

        public async Task<Book> GetById(int id) => await _context.Books.Include(b => b.Author).FirstOrDefaultAsync();

        public async Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }
    }
}
