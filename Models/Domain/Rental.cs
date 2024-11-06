using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain;

[Serializable]
public class Rental
{
    [Key] public int Id { get; set; }
    public int CustomerId { get; set; }
    public int BookId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Member? Customer { get; set; }
    public Book? Book { get; set; }
}
