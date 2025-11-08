using Jadev.Library.Managment.Models;

namespace Jadev.Library.Managment.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Add(Book book);
        Task DeleteById(int id);
        Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId);
    }
}
