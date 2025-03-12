using AutoMapper;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;
[Route(ApiEndPointConstant.Child.ChildEndPoint)]
[ApiController]
public class ChildController : BaseController<ChildController>
{
    private readonly IGrowthRecordService _growthRecordService;
    private readonly IChildService _childService;

    public ChildController(ILogger<ChildController> logger, IGrowthRecordService growthRecordService, IChildService childService) : base(logger)
    {
        _growthRecordService = growthRecordService;
        _childService = childService;
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
    public async Task<IActionResult> GetConsultationByDoctorId([FromQuery]int page = 1,[FromQuery] int size = 30,int childId = 0)
    {
        var consultations = await _growthRecordService.GetGrowthRecordByChildIdAsync(page, size, childId);
        return Ok(consultations);
    }
}