using Jadev.Library.Managment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jadev.Library.Managment.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAll();
        Task<Author?> GetAuthorById(int id);
        Task<Author> Add(Author author);
        Task<Author?> Update(int id, Author author);
        Task<bool> Delete(int id);
        Task<bool> ExistsByName(string name);
        Task<Book?> AddBookToAuthorById(int authorId, Book book);
    }
}
