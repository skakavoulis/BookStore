using BookStore.Data;
using BookStore.Models;
using BookStore.Models.Domain;
using BookStore.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStore.Services;

public class BooksService(BookStoreDbContext context) : IBooksService
{
    public async Task<BookDto> GetBook(int id)
    {
        var book = await context.Books.Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id);
        if (book == null)
            return null;
        return book.Convert();
    }

    public async Task<List<BookDto>> GetAllBooks()
    {
        return await context.Books.Include(a => a.Author).Select(b => b.Convert()).ToListAsync();
    }

    public async Task<List<BookDto>> Search(
        string? name,
        string? publisher,
        string? authorFirst,
        string? authorLast
    )
    {
        var results = context.Books.Include(a => a.Author).Select(b => b);
        if (name != null)
        {
            results = results.Where(b => b.Name.ToLower().Contains(name.ToLower()));
        }

        if (publisher != null)
        {
            results = results.Where(b => b.Publisher.ToLower().Contains(publisher.ToLower()));
        }

        if (authorFirst != null)
        {
            results = results.Where(b =>
                b.Author.FirstName.ToLower().Contains(authorFirst.ToLower())
            );
        }

        if (authorLast != null)
        {
            results = results.Where(b =>
                b.Author != null && b.Author.LastName.ToLower().Contains(authorLast.ToLower())
            );
        }

        var resultsList = await results.ToListAsync();

        List<BookDto> response = new();
        foreach (var b in resultsList)
        {
            response.Add(b.Convert());
        }

        return response;
    }

    public async Task<BookRentalDto?> GetRental(int bookId)
    {
        var result = await context
            .Books.Include(a => a.Author)
            .Include(b => b.RentedTo)
            .SingleOrDefaultAsync(b => b.Id == bookId);
        if (result == null)
            throw new NotFoundException("The book id is invalid.");
        if (result.RentedTo == null)
            return null;
        return result.ConvertRental();
    }

    public async Task<BookDto?> AddBook(BookDto dto)
    {
        var bookAuthor = await context.Authors.SingleOrDefaultAsync(a =>
            dto.Author != null
            && a.FirstName == dto.Author.FirstName
            && a.LastName == dto.Author.LastName
        );
        if (bookAuthor == null)
        {
            var res = await context.Authors.AddAsync(
                new Author() { FirstName = dto.Author.FirstName, LastName = dto.Author.LastName }
            );
            bookAuthor = res.Entity;
        }

        Book book = new Book()
        {
            Name = dto.Name,
            Publisher = dto.Publisher,
            Author = bookAuthor,
        };
        context.Books.Add(book);
        await context.SaveChangesAsync();
        return book.Convert();
    }

    public async Task<BookDto> Update(int bookId, BookDto dto)
    {
        var book = await context
            .Books.Include(b => b.Author)
            .SingleOrDefaultAsync(b => b.Id == bookId);
        if (book is null)
            throw new NotFoundException("The book id is invalid or the book has been removed.");
        if (dto.Name is not null)
            book.Name = dto.Name;
        if (dto.Publisher is not null)
            book.Publisher = dto.Publisher;
        if (dto.Author is not null)
        {
            var author = context.Authors.SingleOrDefault(a => a.Id == dto.Author.Id);
            if (author is not null)
                book.Author = author;
        }

        await context.SaveChangesAsync();
        return book.Convert();
    }

    public async Task<BookDto> Replace(int bookId, BookDto dto)
    {
        var book = await context
            .Books.Include(b => b.Author)
            .SingleOrDefaultAsync(b => b.Id == bookId);
        if (book is null)
            throw new NotFoundException("The book id is invalid");
        if (book.Author is null)
            throw new NotFoundException("An author must be specified.");
        var author = await context.Authors.SingleOrDefaultAsync(a =>
            dto.Author != null && a.Id == dto.Author.Id
        );
        if (author is null)
            throw new NotFoundException("The author id is invalid.");

        //If a property does not exist in the request body, it will become null
        book.Name = dto.Name;
        book.Publisher = dto.Publisher;
        book.Author = author;
        await context.SaveChangesAsync();
        return book.Convert();
    }

    public async Task<bool> Delete(int id)
    {
        var book = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
        if (book is null)
            return false;

        context.Remove(book);
        await context.SaveChangesAsync();
        return true;
    }
}
