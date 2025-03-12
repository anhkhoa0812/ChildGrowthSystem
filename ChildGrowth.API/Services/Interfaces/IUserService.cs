using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;

namespace ChildGrowth.API.Services.Interfaces;

public interface IUserService
{
    Task<SignUpResponse> SignUp(SignUpRequest request);
}