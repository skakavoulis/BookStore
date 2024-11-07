namespace BookStore.Models.Dto;

public class RentalDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int BookId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
