using ChildGrowth.API.Constants;
using ChildGrowth.API.Services.Interfaces;
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
}