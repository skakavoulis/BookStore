using BookStore.Models.Dto;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Author = BookStore.Models.Domain.Author;

namespace BookStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorsService authorsService) : ControllerBase
{
    [HttpGet]
    public CustomResponseDto<IEnumerable<AuthorDto>> GetAuthors()
    {
        var authors = authorsService.GetAllAuthors();
        return new CustomResponseDto<IEnumerable<AuthorDto>>
        {
            Success = true,
            Message = "Authors fetched successfully",
            Data = authors,
        };
    }

    [HttpGet("{id:int}")]
    public CustomResponseDto<AuthorDto> GetAuthor(int id)
    {
        var author = authorsService.GetAuthor(id);
        return new CustomResponseDto<AuthorDto>
        {
            Success = true,
            Message = "Author fetched successfully",
            Data = author,
        };
    }

    [HttpPost]
    public CustomResponseDto<AuthorDto> CreateAuthor(AuthorDto author)
    {
        var createdAuthor = authorsService.CreateAuthor(author);
        return new CustomResponseDto<AuthorDto>
        {
            Success = true,
            Message = "Author created successfully",
            Data = createdAuthor,
        };
    }

    [HttpPut("{id:int}")]
    public CustomResponseDto<AuthorDto?> UpdateAuthor(int id, AuthorDto author)
    {
        var updatedAuthor = authorsService.UpdateAuthor(id, author);
        if (updatedAuthor is null)
        {
            return new CustomResponseDto<AuthorDto?>
            {
                Success = false,
                Message = "Author not found",
                Data = null,
            };
        }

        return new CustomResponseDto<AuthorDto?>
        {
            Success = true,
            Message = "Author updated successfully",
            Data = updatedAuthor,
        };
    }

    [HttpDelete("{id:int}")]
    public CustomResponseDto<bool> DeleteAuthor(int id)
    {
        var result = authorsService.DeleteAuthor(id);
        return new CustomResponseDto<bool>
        {
            Success = true,
            Message = "Author deleted successfully",
            Data = result,
        };
    }
}
