using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Dto;
using BookStore.Services;

namespace BookStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBooksService booksService) : ControllerBase
{
    [HttpGet]
    public async Task<CustomResponseDto<IEnumerable<BookDto>>> GetBooks()
    {
        var books = await booksService.GetAllBooks();

        return new CustomResponseDto<IEnumerable<BookDto>>
        {
            Success = true,
            Message = "Books fetched successfully",
            Data = books
        };
    }

    [HttpGet("{id:int}")]
    public async Task<CustomResponseDto<BookDto>> GetBook(int id)
    {
        var res = await booksService.GetBook(id);
        if (!string.IsNullOrWhiteSpace(res.Item2))
        {
            return new CustomResponseDto<BookDto>
            {
                Success = false,
                Message = res.Item2,
                Data = null
            };
        }

        return new CustomResponseDto<BookDto>
        {
            Success = true,
            Message = "Book fetched successfully",
            Data = res.Item1
        };
    }

    [HttpGet("search")]
    public async Task<CustomResponseDto<List<BookDto>>> Search
    ([FromQuery] string name,
        [FromQuery] string publisher,
        [FromQuery] string authorFirst,
        [FromQuery] string authorLast)
    {
        return new CustomResponseDto<List<BookDto>>
        {
            Success = true,
            Message = "Books fetched successfully",
            Data = await booksService.Search(name, publisher, authorFirst, authorLast)
        };
    }

    [HttpPost]
    public async Task<CustomResponseDto<BookDto>> AddBook(BookDto bookDto)
    {
        return new CustomResponseDto<BookDto>
        {
            Success = true,
            Message = "Books fetched successfully",
            Data = await booksService.AddBook(bookDto)
        };
    }

    [HttpPut("{id:int}")]
    public async Task<CustomResponseDto<BookDto>> UpdateBook(int id, BookDto bookDto)
    {
        return new CustomResponseDto<BookDto>
        {
            Success = true,
            Message = "Books fetched successfully",
            Data = await booksService.Replace(id, bookDto)
        };
    }

    [HttpPatch("{id:int}")]
    public async Task<CustomResponseDto<BookDto>> PartiallyUpdateBook(int id, BookDto bookDto)
    {
        return new CustomResponseDto<BookDto>
        {
            Success = true,
            Message = "Books fetched successfully",
            Data = await booksService.Update(id, bookDto)
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<CustomResponseDto<bool>> DeleteBook(int id)
    {
        return new CustomResponseDto<bool>
        {
            Success = true,
            Message = "Books fetched successfully",
            Data = await booksService.Delete(id)
        };
    }
}