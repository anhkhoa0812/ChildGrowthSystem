using ChildGrowth.API.Constants;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Validators;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;


[Route("api/[controller]")]

[ApiController]
public class UserController : BaseController<UserController>
{
    private readonly IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }
    [HttpGet(ApiEndPointConstant.User.UserEndPoint)]
    [ProducesResponseType(typeof(IPaginate<UserResponse>), StatusCodes.Status200OK)]
    [CustomAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> GetAllUser([FromQuery]int page = 1,[FromQuery] int size = 30)
    {
        var users = await _userService.GetUserAsync(page, size);
        return Ok(users);
    }
    [HttpPost(ApiEndPointConstant.User.SignUp)]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var response = await _userService.SignUp(request);
        return Ok(response);
    }
    
    [HttpPost(ApiEndPointConstant.User.SignIn)]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    { var response = await _userService.SignIn(request);
            return Ok(response);
    }
    
}