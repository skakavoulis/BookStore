using BookStore.Controllers;
using BookStore.Models.Dto;
using Rental = BookStore.Models.Domain.Rental;

namespace BookStore.Services;

public interface IRentalsService
{
    IEnumerable<Rental> GetAllRentals();
    Rental RentBook(int customerId, int bookId);
    bool ReturnBook(int customerId, int rentalId);
}
