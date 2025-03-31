using System.Security.Claims;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.API.Validators;
using ChildGrowth.Domain.Enum;
using ChildGrowth.Domain.Filter.ModelFilter;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;


[Route("api/[controller]")]

[ApiController]
public class UserController : BaseController<UserController>
{
    private readonly IUserService _userService;
    private readonly IConsultationService _consultationService;
    private readonly IChildService _childService;
    public UserController(ILogger<UserController> logger, IUserService userService, IConsultationService consultationService, IChildService childService) : base(logger)
    {
        _userService = userService;
        _consultationService = consultationService;
        _childService = childService;
    }
    [HttpGet(ApiEndPointConstant.User.UserEndPoint)]
    [ProducesResponseType(typeof(IPaginate<UserResponse>), StatusCodes.Status200OK)]
    [CustomAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> GetAllUser([FromQuery]int page = 1,[FromQuery] int size = 30, [FromQuery] UserFilter? filter = null,[FromQuery] string? sortBy = null,[FromQuery] bool isAsc = true )
    {
        var users = await _userService.GetUserAsync(page, size, filter, sortBy, isAsc);
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
    
    [HttpGet(ApiEndPointConstant.User.ById)]
    [CustomAuthorize(RoleEnum.Member, RoleEnum.Admin)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }
    
    [HttpPut(ApiEndPointConstant.User.ById)]
    [CustomAuthorize(RoleEnum.Member, RoleEnum.Admin)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request)
    {
        var result = await _userService.UpdateUserAsync(id, request);
        return Ok(result);
    }
    [HttpGet(ApiEndPointConstant.User.Consultations)]
    [CustomAuthorize(RoleEnum.Member)]
    [ProducesResponseType(typeof(IPaginate<ConsultationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsultations([FromQuery] int page = 1, [FromQuery] int size = 30, [FromQuery] ConsultationFilter? filter = null, [FromQuery] string? sortBy = null, [FromQuery] bool isAsc = false)
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId);
        var consultations = await _consultationService.GetConsultationsByParentId(parentIdInt, page, size, filter, sortBy, isAsc);
        return Ok(consultations);
    }
    [HttpGet(ApiEndPointConstant.User.ConsultationById)]
    [CustomAuthorize(RoleEnum.Member)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsultationById(int id)
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId);
        var result = await _consultationService.GetConsultationByIdWithParentIdAsync(id, parentIdInt);
        return Ok(result);
    }
    [HttpGet(ApiEndPointConstant.User.Children)]
    [CustomAuthorize(RoleEnum.Member)]
    [ProducesResponseType(typeof(List<ChildResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChildren()
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId);
        var response = await _childService.GetChildrenByParentIdAsync(parentIdInt);
        return Ok(response);
    }
    
}