using Jadev.Library.Managment.enums;

namespace Jadev.Library.Managment.Dtos
{
    public class BookResDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public BookStatus Status { get; set; }
    }
}
