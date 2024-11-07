using BookStore.Models.Dto;

namespace BookStore.Services;

public interface IBooksService
{
    public Task<Tuple<BookDto?, string>> GetBook(int id);
    public Task<List<BookDto>> GetAllBooks();
    public Task<BookDto?> AddBook(BookDto dto);

    public Task<List<BookDto>> Search
        (string name, string publisher, string authorFirst, string authorLast);

    public Task<BookRentalDto?> GetRental(int bookId);
    public Task<BookDto> Update(int bookId, BookDto dto);
    public Task<BookDto> Replace(int bookId, BookDto dto);
    public Task<bool> Delete(int bookId);
}
