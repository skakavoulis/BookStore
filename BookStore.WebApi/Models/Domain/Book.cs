using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain;

[Serializable]
public class Book
{
    [Key] public int Id { get; set; }

    [MaxLength(200)] public string? Name { get; set; }

    [MaxLength(100)] public string? Isbn { get; set; }
    public int PublicationYear { get; set; }

    [MaxLength(50)] public string? Genre { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public int? RentedToId { get; set; }
    public Member? RentedTo { get; set; }
    public ICollection<Rental> Rentals { get; set; } = [];
    public string? Publisher { get; set; }
}
