using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

[Route(ApiEndPointConstant.User.UserEndPoint)]
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
    public async Task<IActionResult> GetConsultationByDoctorId([FromQuery]int page = 1,[FromQuery] int size = 30)
    {
        var users = await _userService.GetUserAsync(page, size);
        return Ok(users);
    }
}