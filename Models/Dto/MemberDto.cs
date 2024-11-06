namespace BookStore.Models.Dto;

public class MemberDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public MemberDto? RentedTo { get; set; }
}
