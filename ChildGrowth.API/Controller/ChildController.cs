using AutoMapper;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Request.Children;
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
    private readonly IMapper _mapper;

    public ChildController(ILogger<ChildController> logger, IGrowthRecordService growthRecordService, IChildService childService, IGrowthAlertService growthAlertService, IMapper mapper) : base(logger)
    {
        _growthRecordService = growthRecordService;
        _childService = childService;
        _growthAlertService = growthAlertService;
        _mapper = mapper;
    }
    [HttpGet(ApiEndPointConstant.Child.ChildEndPoint)]
    [ProducesResponseType(typeof(IPaginate<ChildResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllChildren([FromQuery]int page = 1,[FromQuery] int size = 30)
    {
        var childrens = await _childService.GetAllChildren(page, size);
        return Ok(childrens);
    }
    [HttpGet(ApiEndPointConstant.Child.GetById)]
    [ProducesResponseType(typeof(ChildResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChildById([FromRoute] int childId)
    {
        var child = await _childService.GetChildByIdAsync(childId);
        if (child == null)
            return NotFound();

        return Ok(child);
    }
    [HttpPost(ApiEndPointConstant.Child.Create)]
    [ProducesResponseType(typeof(ChildResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateChild([FromBody] CreateChildrenRequest request)
    {
        var newChild = await _childService.CreateChildAsync(request);
        return CreatedAtAction(nameof(CreateChild), newChild);
    }
    [HttpPut(ApiEndPointConstant.Child.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateChild([FromRoute] int childId, [FromBody] UpdateChildrenRequest request)
    {
        await _childService.UpdateChildAsync(childId, request);
        

        return NoContent();
    }
    [HttpDelete(ApiEndPointConstant.Child.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteChild([FromRoute] int childId)
    {
        var deleted = await _childService.DeleteChildAsync(childId);
        if (!deleted)
            return NotFound();

        return NoContent();
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