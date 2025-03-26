using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Request.MembershipPlan;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Validators;
using ChildGrowth.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;


[Route("api/[controller]")]

[ApiController]
public class MembershipPlanController : BaseController<MembershipPlanController>
{
    private readonly IMembershipPlanService _membershipPlanService;
    public MembershipPlanController(ILogger<MembershipPlanController> logger, IMembershipPlanService membershipPlanService) : base(logger)
    {
        _membershipPlanService = membershipPlanService;
    }
    
    [HttpGet(ApiEndPointConstant.MembershipPlan.MembershipPlanEndPoint)]
    public async Task<IActionResult> GetMembershipPlans()
    {
        var result = await _membershipPlanService.GetMembershipPlans();
        return Ok(result);
    }
    
    [HttpGet(ApiEndPointConstant.MembershipPlan.GetMembershipPlanById)]
    public async Task<IActionResult> GetMembershipPlanById(int id)
    {
        var result = await _membershipPlanService.GetMembershipPlanById(id);
        return Ok(result);
    }
    
    [HttpPost(ApiEndPointConstant.MembershipPlan.CreateMembershipPlan)]
    [CustomAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> CreateMembershipPlan([FromBody] CreateMembershipPlanRequest request)
    {
        var result = await _membershipPlanService.CreateMembershipPlan(request);
        return Ok(result);
    }
    
    [HttpPut(ApiEndPointConstant.MembershipPlan.UpdateMembershipPlan)]
    [CustomAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> UpdateMembershipPlan([FromBody] UpdateMembershipPlanRequest request)
    {
        var result = await _membershipPlanService.UpdateMembershipPlan(request);
        return Ok(result);
    }
    
    [HttpDelete(ApiEndPointConstant.MembershipPlan.InactiveMembershipPlan)]
    [CustomAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> InactiveMembershipPlan(int id)
    {
        var result = await _membershipPlanService.InactiveMembershipPlan(id);
        return Ok(result);
    }
}