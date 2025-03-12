using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.API.Payload.Response.User;


namespace ChildGrowth.API.Services.Interfaces;

public interface IUserService
{
    Task<SignInResponse> SignUp(SignUpRequest request);
    Task<SignInResponse> SignIn(SignInRequest request);
    Task<IPaginate<UserResponse>> GetUserAsync(int page, int size);
}