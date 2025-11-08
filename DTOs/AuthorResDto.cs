using System.Collections.Generic;

namespace Jadev.Library.Managment.Dtos
{
    public sealed class AuthorResDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public List<BookResDTO>? Books { get; set; }
    }
}




