using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.Domain.Filter.ModelFilter;
using ChildGrowth.Domain.Paginate;


namespace ChildGrowth.API.Services.Interfaces;

public interface IUserService
{
    Task<SignInResponse> SignUp(SignUpRequest request);
    Task<SignInResponse> SignIn(SignInRequest request);
    Task<IPaginate<UserResponse>> GetUserAsync(int page, int size, UserFilter filter, string? sortBy, bool isAsc);
}