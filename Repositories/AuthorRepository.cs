using Jadev.Library.Managment.Data;
using Jadev.Library.Managment.enums;
using Jadev.Library.Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Jadev.Library.Managment.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context) => _context = context;

        public async Task<IEnumerable<Author>> GetAll() => await _context.Authors
            .Include(a => a.Books)
            .ToListAsync();

        public async Task<Author?> GetAuthorById(int id) => await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Author> Add(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> Update(int id, Author author)
        {
            var existingAuthor = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existingAuthor == null)
                return null; 

            existingAuthor.Name = author.Name;
            existingAuthor.Biography = author.Biography;

            if (existingAuthor.Books != null && author.Books != null && author.Books.Any())
            {
                _context.Books.RemoveRange(existingAuthor.Books);
                existingAuthor.Books = author.Books.Select(b => new Book
                {
                    Title = b.Title,
                    Description = b.Description,
                    PublishedDate = b.PublishedDate,
                    AuthorId = existingAuthor.Id, 
                    Status = b.Status
                }).ToList();
            }

            await _context.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<bool> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return false; 

            _context.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Authors
                .AnyAsync(a => a.Name.ToLower() == name.ToLower());
        }

        public async Task<Book?> AddBookToAuthorById(int authorId, Book book)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == authorId);

            if (author == null)
                return null; 

            book.AuthorId = authorId;
            book.Status = BookStatus.Available;
            //author.Books ??= new List<Book>();
            author.Books.Add(book);

            await _context.SaveChangesAsync();
            return book;
        }
    }
}