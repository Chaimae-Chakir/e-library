using Jadev.Library.Managment.Repositories;
using Jadev.Library.Managment.Models;
using Jadev.Library.Managment.Dtos;
using Jadev.Library.Managment.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Jadev.Library.Managment.Exceptions;

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

        public async Task<AuthorResDto?> GetById(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);
            return author != null ? AuthorMapper.MapToDto(author) : null;
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

        public async Task<AuthorResDto> update(int id,AuthorReqDto reqDto)
        {
            Author author = AuthorMapper.MapToEntity(reqDto);
            Author updated_author = await _authorRepository.Update(id, author);
            return AuthorMapper.MapToDto(updated_author);
        }

        public async Task<bool> delete(int id)
        {
           return await _authorRepository.Delete(id);
        }

        public async Task<IEnumerable<BookResDTO>> GetBooksByAuthorId(int authorId)
        {
            var author = await _authorRepository.GetAuthorById(authorId);
            if (author == null)
                throw new NotFoundException($"this author {authorId} doesn't exists");
            IEnumerable<Book> booksByAuthorId = await _bookRepository.GetBooksByAuthorId(author.Id);
            return booksByAuthorId.Select(b => AuthorMapper.MapToBookDTO(b)).ToList();
        }
    }
}
