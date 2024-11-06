using BookStore.Models.Dto;
using Author = BookStore.Models.Domain.Author;

namespace BookStore.Services;

public interface IAuthorsService
{
    IEnumerable<AuthorDto>? GetAllAuthors();
    AuthorDto? GetAuthor(int id);
    AuthorDto? CreateAuthor(AuthorDto author);
    AuthorDto? UpdateAuthor(int id, AuthorDto author);
    bool DeleteAuthor(int id);
}
