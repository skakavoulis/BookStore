using System.ComponentModel.DataAnnotations;
using BookStore.Data;

namespace BookStore.Models.Domain;

[Serializable]
public class Author
{
    [Key] public int Id { get; set; }
    [MaxLength(100)] public required string FirstName { get; set; }
    [MaxLength(100)] public required string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    [MaxLength(500)] public string? Biography { get; set; }
    [MaxLength(20)] public string? Nationality { get; set; }
    public ICollection<Book> Books { get; set; } = [];
}
