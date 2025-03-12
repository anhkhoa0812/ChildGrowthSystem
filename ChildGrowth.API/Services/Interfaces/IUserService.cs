using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IUserService
{
    Task<IPaginate<UserResponse>> GetUserAsync(int page, int size);
}