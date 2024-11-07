using BookStore.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : DbContext(options)
{
    public required DbSet<Book> Books { get; set; }
    public required DbSet<Author> Authors { get; set; }
    public required DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        // Seed data
        modelBuilder
            .Entity<Author>()
            .HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Orwell",
                },
                new Author
                {
                    Id = 2,
                    FirstName = "John Ronald Reuel",
                    LastName = "Tolkien",
                }
            );

        modelBuilder
            .Entity<Book>()
            .HasData(
                new Book
                {
                    Id = 1,
                    Name = "1984",
                    AuthorId = 1,
                    PublicationYear = 1949,
                    Isbn = "978-0451524935",
                    Publisher = "Signet Classics",
                },
                new Book
                {
                    Id = 2,
                    Name = "Animal Farm",
                    AuthorId = 1,
                    PublicationYear = 1945,
                    Isbn = "978-0451526342",
                    Publisher = "Signet Classics",
                },
                new Book
                {
                    Id = 3,
                    Name = "The Hobbit",
                    AuthorId = 2,
                    PublicationYear = 1937,
                    Isbn = "978-0547928227",
                    Publisher = "Houghton Mifflin Harcourt",
                }
            );
    }
}
