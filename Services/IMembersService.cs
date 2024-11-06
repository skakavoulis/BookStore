using BookStore.Models.Domain;
using BookStore.Models.Dto;

namespace BookStore.Services;

public interface IMembersService
{
    IEnumerable<Member> GetAllMembers();
    Member? GetMember(int id);
    Member CreateMember(Member member);
    Member? UpdateMember(int id, Member member);
    bool DeleteMember(int id);
}
