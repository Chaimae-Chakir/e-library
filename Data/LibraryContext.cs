using Jadev.Library.Managment.enums;
using Jadev.Library.Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Jadev.Library.Managment.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .Property(b => b.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Name = "George Orwell",
                    Biography = "Auteur britannique connu pour 1984 et La Ferme des animaux."
                },
                new Author
                {
                    Id = 2,
                    Name = "J.K. Rowling",
                    Biography = "Romancière britannique, créatrice de la saga Harry Potter."
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    Description = "Roman dystopique sur le totalitarisme et la surveillance.",
                    PublishedDate = new DateTime(1949, 6, 8),
                    AuthorId = 1, 
                    Status = BookStatus.Available
                },
                new Book
                {
                    Id = 2,
                    Title = "Harry Potter à l'école des sorciers",
                    Description = "Premier tome de la saga Harry Potter.",
                    PublishedDate = new DateTime(1997, 6, 26),
                    AuthorId = 2, 
                    Status = BookStatus.Borrowed
                }
            );
        }
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
    }
}
