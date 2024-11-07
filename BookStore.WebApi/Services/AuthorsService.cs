using BookStore.Data;
using BookStore.Models;
using BookStore.Models.Domain;
using BookStore.Models.Dto;

namespace BookStore.Services;

public class AuthorsService(BookStoreDbContext context) : IAuthorsService
{
    public IEnumerable<AuthorDto>? GetAllAuthors()
    {
        return context.Authors.Select(book => book.Convert()).ToArray();
    }

    public AuthorDto? GetAuthor(int id)
    {
        return context.Authors.Find(id)?.Convert();
    }

    public AuthorDto? CreateAuthor(AuthorDto author)
    {
        var domainModel = new Author { FirstName = author.FirstName, LastName = author.LastName };

        var res = context.Authors.Add(domainModel);
        context.SaveChanges();

        return res.Entity.Convert();
    }

    public AuthorDto? UpdateAuthor(int id, AuthorDto author)
    {
        var existingAuthor = context.Authors.Find(id);
        if (existingAuthor == null)
            throw new NotFoundException("Author not found");

        existingAuthor.FirstName = author.FirstName;
        existingAuthor.LastName = author.LastName;

        context.SaveChanges();
        return existingAuthor.Convert();
    }

    public bool DeleteAuthor(int id)
    {
        var author = context.Authors.Find(id);
        if (author == null)
            return false;

        context.Authors.Remove(author);
        context.SaveChanges();
        return true;
    }
}
