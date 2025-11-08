using Jadev.Library.Managment.enums;

namespace Jadev.Library.Managment.DTOs
{
    public class BookReqDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
    }
}
