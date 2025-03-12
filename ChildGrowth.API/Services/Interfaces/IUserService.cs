using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.API.Payload.Response.User;


namespace ChildGrowth.API.Services.Interfaces;

public interface IUserService
{
    Task<IPaginate<UserResponse>> GetUserAsync(int page, int size);
    Task<SignUpResponse> SignUp(SignUpRequest request);
}