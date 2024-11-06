namespace BookStore.Models.Dto;

public class BookRentalDto : BookDto
{
    public MemberDto RentedTo { get; set; }
}
