using Jadev.Library.Managment.Dtos;
using Jadev.Library.Managment.Models;

namespace Jadev.Library.Managment.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResDto>> GetAll();
        Task<AuthorResDto?> GetById(int id);
        Task<AuthorResDto> Create(AuthorReqDto request);
        Task<AuthorResDto> update(int id, AuthorReqDto reqDto);
        Task<bool> delete(int id);
        Task<IEnumerable<BookResDTO>> GetBooksByAuthorId(int authorId);
    }
}
