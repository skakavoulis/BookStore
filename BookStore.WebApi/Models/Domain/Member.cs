using System.ComponentModel.DataAnnotations;
using BookStore.Data;

namespace BookStore.Models.Domain;

[Serializable]
public class Member
{
    [Key] public int Id { get; set; }
    [MaxLength(100)] public required string FirstName { get; set; }
    [MaxLength(100)] public string? LastName { get; set; }
    [MaxLength(100)] public string? Email { get; set; }
    [MaxLength(20)] public string? PhoneNumber { get; set; }
    [MaxLength(100)] public string? Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<Rental> Rentals { get; set; } = [];
}
