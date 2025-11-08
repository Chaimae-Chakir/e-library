using Jadev.Library.Managment.Dtos;
using Jadev.Library.Managment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jadev.Library.Managment.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService) => _authorService = authorService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResDto>>> GetAllAsync()
        {
            var authors = await _authorService.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResDto>> GetByIdAsync(int id)
        {
            var author = await _authorService.GetById(id);
            return author != null ? Ok(author) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AuthorResDto>> CreateAsync([FromBody] AuthorReqDto request)
        {
            var author = await _authorService.Create(request);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorResDto>> UpdateAsync(int id, AuthorReqDto dto)
        {  
                var updated = await _authorService.update(id, dto);
                return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted= await _authorService.delete(id);
            if(!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{authorId}/books")]
        public async Task<ActionResult<IEnumerable<BookResDTO>>> GetAllBooksAsync(int authorId)
        {
           return Ok(await _authorService.GetBooksByAuthorId(authorId));
        }
    }
}