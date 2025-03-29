using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Services.Interfaces;

public interface IUserMembershipService
{
    Task<IEnumerable<UserMembership>> GetUserMemberships();
    Task<UserMembership> GetUserMembershipById(int id);
}