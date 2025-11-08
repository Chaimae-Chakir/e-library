using Jadev.Library.Managment.Dtos;
using Jadev.Library.Managment.DTOs;
using Jadev.Library.Managment.Models;

namespace Jadev.Library.Managment.Mappers
{
    public class AuthorMapper
    {
        public AuthorMapper() { }
        public static AuthorResDto MapToDto(Author author)
        {
            return new AuthorResDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                Books = author.Books?.Select(b => new BookResDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    PublishedDate = b.PublishedDate
                }).ToList()
            };
        }

        public static Author MapToEntity(AuthorReqDto dto)
        {
            return new Author
            {
                Name = dto.Name,
                Biography = dto.Biography
            };
        }

        public static BookResDTO MapToBookDTO(Book book)
        {
            return new BookResDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Status = book.Status.ToString(),
            };
        }

        public static Book MapToBook(BookReqDTO bookReqDTO)
        {
            return new Book
            {
                Title = bookReqDTO.Title,
                Description = bookReqDTO.Description,
                PublishedDate = bookReqDTO.PublishedDate
            };
        }
    }
}
