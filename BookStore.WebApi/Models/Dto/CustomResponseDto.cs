namespace BookStore.Models.Dto;

public class CustomResponseDto<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}
