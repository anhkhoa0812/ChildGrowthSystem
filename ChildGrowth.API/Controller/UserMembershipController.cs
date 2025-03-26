using ChildGrowth.API.Constants;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserMembershipController : BaseController<UserMembershipController>
{
    private readonly IUserMembershipService _userMembershipService;
    public UserMembershipController(ILogger<UserMembershipController> logger, IUserMembershipService userMembershipService) : base(logger)
    {
        _userMembershipService = userMembershipService;
    }
    
    [HttpGet(ApiEndPointConstant.UserMembership.UserMembershipEndPoint)]
    [CustomAuthorize(RoleEnum.Member, RoleEnum.Admin)]
    public async Task<IActionResult> GetUserMemberships()
    {
        var result = await _userMembershipService.GetUserMemberships();
        return Ok(result);
    }
    
    [HttpGet(ApiEndPointConstant.UserMembership.UserMembershipByIdEndPoint)]
    [CustomAuthorize(RoleEnum.Member, RoleEnum.Admin)]
    public async Task<IActionResult> GetUserMembershipById(int id)
    {
        var result = await _userMembershipService.GetUserMembershipById(id);
        return Ok(result);
    }
}