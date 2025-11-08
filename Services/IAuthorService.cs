using Jadev.Library.Managment.Dtos;
using Jadev.Library.Managment.DTOs;
using Jadev.Library.Managment.Models;

namespace Jadev.Library.Managment.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResDto>> GetAll();
        Task<AuthorResDto> GetById(int id);
        Task<AuthorResDto> Create(AuthorReqDto request);
        Task<AuthorResDto> Update(int id, AuthorReqDto reqDto);
        Task Delete(int id);
        Task<IEnumerable<BookResDTO>> GetBooksByAuthorId(int authorId);
        Task<BookResDTO> AddBookToAuthorById(int authorId, BookReqDTO bookDTO);
    }
}
