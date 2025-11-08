using Jadev.Library.Managment.Data;
using Jadev.Library.Managment.Exceptions;
using Jadev.Library.Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Jadev.Library.Managment.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;

       public AuthorRepository(LibraryContext context) =>_context = context;

        public async Task<IEnumerable<Author>> GetAll() => await _context.Authors
            .Include(a => a.Books)
            .ToListAsync();

        public async Task<Author> GetAuthorById(int id) => await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        public async Task<Author> Add(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Update(int id, Author author)
        {
            //_context.Entry(author).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            var existingAuthor = await _context.Authors.Include(a=>a.Books).FirstOrDefaultAsync(a=>a.Id == id);
            if (existingAuthor == null)
                throw new NotFoundException("the author doesn't exist");
         
            existingAuthor.Name = author.Name;
            existingAuthor.Biography = author.Biography;
            if (existingAuthor.Books != null && author.Books.Any())
            {
                _context.Books.RemoveRange(existingAuthor.Books);
                existingAuthor.Books = author.Books.Select(b => new Book
                {
                    Title = b.Title,
                    Description = b.Description,
                    PublishedDate = b.PublishedDate,
                    AuthorId = b.AuthorId,
                    Status = b.Status,
                    Author = b.Author
                }).ToList();
            }
            await _context.SaveChangesAsync();
            return existingAuthor;
        }
        public async Task<bool> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                throw new NotFoundException("Author doesn't exists");
            _context.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Authors
                .AnyAsync(a => a.Name.ToLower() == name.ToLower());
        }
    }
}
