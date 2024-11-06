namespace BookStore.Models.Dto;

public class BookDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public AuthorDto? Author { get; set; }
    public string? Publisher { get; set; }
}
