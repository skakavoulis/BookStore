using BookStore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Dto;
using BookStore.Services;

namespace BookStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(IMembersService MembersService)
    : ControllerBase
{
    [HttpGet]
    public CustomResponseDto<IEnumerable<Member>> GetMembers()
    {
        var Members = MembersService.GetAllMembers();
        return new CustomResponseDto<IEnumerable<Member>>
        {
            Success = true,
            Message = "Members fetched successfully",
            Data = Members
        };
    }

    [HttpGet("{id:int}")]
    public CustomResponseDto<Member> GetMember(int id)
    {
        var Member = MembersService.GetMember(id);
        if (Member == null)
        {
            return new CustomResponseDto<Member>
            {
                Success = false,
                Message = "Member not found",
                Data = null
            };
        }

        return new CustomResponseDto<Member>
        {
            Success = true,
            Message = "Member fetched successfully",
            Data = Member
        };
    }

    [HttpPost]
    public CustomResponseDto<Member> CreateMember(Member member)
    {
        var createdMember = MembersService.CreateMember(member);
        return new CustomResponseDto<Member>
        {
            Success = true,
            Message = "Member created successfully",
            Data = createdMember
        };
    }

    [HttpPut("{id:int}")]
    public CustomResponseDto<Member> UpdateMember(int id, Member member)
    {
        var updatedMember = MembersService.UpdateMember(id, member);
        if (updatedMember == null)
        {
            return new CustomResponseDto<Member>
            {
                Success = false,
                Message = "Member not found",
                Data = null
            };
        }

        return new CustomResponseDto<Member>
        {
            Success = true,
            Message = "Member updated successfully",
            Data = updatedMember
        };
    }

    [HttpDelete("{id:int}")]
    public CustomResponseDto<object> DeleteMember(int id)
    {
        var result = MembersService.DeleteMember(id);
        if (!result)
        {
            return new CustomResponseDto<object>
            {
                Success = false,
                Message = "Member not found",
                Data = null
            };
        }

        return new CustomResponseDto<object>
        {
            Success = true,
            Message = "Member deleted successfully",
            Data = null
        };
    }
}
