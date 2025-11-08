using Jadev.Library.Managment.Models;

namespace Jadev.Library.Managment.Repositories
{
    public interface IBookRepository
    {
        Task<Book> Add(Book book);
        Task<Book?> Update(Book book);
        Task<bool> DeleteById(int id);
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId);

    }
}
