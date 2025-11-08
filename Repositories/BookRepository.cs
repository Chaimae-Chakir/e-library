using Jadev.Library.Managment.Data;
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

        public async Task<Book?> Update(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);
            if (existingBook == null)
                return null;

            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.AuthorId = book.AuthorId;
            existingBook.Status = book.Status;

            await _context.SaveChangesAsync();
            return existingBook;
        }

        public async Task<bool> DeleteById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false; 

            _context.Remove(book);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<IEnumerable<Book>> GetAll() => await _context.Books
            .Include(b => b.Author)
            .ToListAsync();

        public async Task<Book?> GetById(int id) => await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id); 

        public async Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }
    }
}