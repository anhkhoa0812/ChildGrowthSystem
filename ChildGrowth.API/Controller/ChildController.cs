using AutoMapper;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.API.Payload.Response.GrowthAlert;
using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Validators;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;
[Route(ApiEndPointConstant.Child.ChildEndPoint)]
[ApiController]
public class ChildController : BaseController<ChildController>
{
    private readonly IGrowthRecordService _growthRecordService;
    private readonly IGrowthAlertService _growthAlertService;
    private readonly IChildService _childService;

    public ChildController(ILogger<ChildController> logger, IGrowthRecordService growthRecordService, IChildService childService, IGrowthAlertService growthAlertService) : base(logger)
    {
        _growthRecordService = growthRecordService;
        _childService = childService;
        _growthAlertService = growthAlertService;  
    }
    [HttpGet(ApiEndPointConstant.Child.ChildEndPoint)]
    [ProducesResponseType(typeof(IPaginate<ChildResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllChildren([FromQuery]int page = 1,[FromQuery] int size = 30)
    {
        var consultations = await _childService.GetAllChildren(page, size);
        return Ok(consultations);
    }
    [HttpGet(ApiEndPointConstant.Child.GrowthRecordChild)]
    [ProducesResponseType(typeof(IPaginate<GrowthRecordResponse>), StatusCodes.Status200OK)]
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Member)]
    public async Task<IActionResult> GetGrowthRecordByChildId([FromQuery]int page = 1,[FromQuery] int size = 30,int childId = 0)
    {
        var growthRecords = await _growthRecordService.GetGrowthRecordByChildIdAsync(page, size, childId);
        return Ok(growthRecords);
    }
    
    [HttpGet(ApiEndPointConstant.Child.GrowthAlertChild)]
    [ProducesResponseType(typeof(IPaginate<GrowthAlertResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGrowthAlertByChildId([FromQuery]int page = 1,[FromQuery] int size = 30,int childId = 0)
    {
        var growthAlerts = await _growthAlertService.GetGrowthAlertByChildIdAsync(page, size, childId);
        return Ok(growthAlerts);
    }
    
    
}