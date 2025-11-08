using Jadev.Library.Managment.enums;

namespace Jadev.Library.Managment.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title {  get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public BookStatus Status { get; set; } = BookStatus.Available;

    }
}
