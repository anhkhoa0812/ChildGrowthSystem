using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;

namespace ChildGrowth.API.Services.Interfaces;

public interface IUserService
{
    Task<SignInResponse> SignUp(SignUpRequest request);
    Task<SignInResponse> SignIn(SignInRequest request);
}