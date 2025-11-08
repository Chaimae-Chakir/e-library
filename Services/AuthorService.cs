using Jadev.Library.Managment.Repositories;
using Jadev.Library.Managment.Models;
using Jadev.Library.Managment.Dtos;
using Jadev.Library.Managment.Mappers;
using Jadev.Library.Managment.Exceptions;
using Jadev.Library.Managment.DTOs;

namespace Jadev.Library.Managment.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<AuthorResDto>> GetAll()
        {
            var authors = await _authorRepository.GetAll();
            return authors.Select(AuthorMapper.MapToDto).ToList();
        }

        public async Task<AuthorResDto> GetById(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);
            if (author == null)
                throw new NotFoundException($"Author with ID {id} not found");

            return AuthorMapper.MapToDto(author);
        }

        public async Task<AuthorResDto> Create(AuthorReqDto request)
        {
            var author = new Author
            {
                Name = request.Name,
                Biography = request.Biography
            };

            var createdAuthor = await _authorRepository.Add(author);
            return AuthorMapper.MapToDto(createdAuthor);
        }

        public async Task<AuthorResDto> Update(int id, AuthorReqDto reqDto)
        {
            var author = AuthorMapper.MapToEntity(reqDto);
            var updatedAuthor = await _authorRepository.Update(id, author);

            if (updatedAuthor == null)
                throw new NotFoundException($"Author with ID {id} not found");

            return AuthorMapper.MapToDto(updatedAuthor);
        }

        public async Task Delete(int id)
        {
            var deleted = await _authorRepository.Delete(id);
            if (!deleted)
                throw new NotFoundException($"Author with ID {id} not found");
        }

        public async Task<IEnumerable<BookResDTO>> GetBooksByAuthorId(int authorId)
        {
            var author = await _authorRepository.GetAuthorById(authorId);
            if (author == null)
                throw new NotFoundException($"Author with ID {authorId} not found");

            var booksByAuthorId = await _bookRepository.GetBooksByAuthorId(authorId);
            return booksByAuthorId.Select(b => AuthorMapper.MapToBookDTO(b)).ToList();
        }

        public async Task<BookResDTO> AddBookToAuthorById(int authorId, BookReqDTO bookDTO)
        {
            var book = AuthorMapper.MapToBook(bookDTO);
            var addedBook = await _authorRepository.AddBookToAuthorById(authorId, book);

            if (addedBook == null)
                throw new NotFoundException($"Author with ID {authorId} not found");

            return AuthorMapper.MapToBookDTO(addedBook);
        }
    }
}