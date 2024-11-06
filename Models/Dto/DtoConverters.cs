using BookStore.Models.Domain;

namespace BookStore.Models.Dto;

public static class DtoConverters
{
    public static BookDto Convert(this Book book)
    {
        AuthorDto? author = null;
        if (book.Author != null)
        {
            author = new AuthorDto
            {
                Id = book.Author.Id,
                FirstName = book.Author.FirstName,
                LastName = book.Author.LastName
            };
        }

        return new BookDto()
        {
            Id = book.Id,
            Name = book.Name,
            Publisher = book.Publisher,
            Author = author
        };
    }

    public static BookRentalDto ConvertRental(this Book book)
    {
        return new BookRentalDto()
        {
            Id = book.Id,
            Name = book.Name,
            Publisher = book.Publisher,
            Author = new()
            {
                FirstName = book.Author.FirstName,
                LastName = book.Author.LastName
            },
            RentedTo = new MemberDto()
            {
                Id = book.RentedTo.Id,
                FirstName = book.RentedTo.FirstName,
                LastName = book.RentedTo.LastName
            }
        };
    }

    public static AuthorDto Convert(this Author author)
    {
        return new AuthorDto()
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        };
    }
}
