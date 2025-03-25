using System.Security.Claims;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.API.Payload.Response.Doctor;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Validators;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

[Route(ApiEndPointConstant.Doctor.DoctorEndPoint)]
[ApiController]
public class DoctorController : BaseController<DoctorController>
{
    private readonly IConsultationService _consultationService;
    private readonly IChildService _childService;
    public DoctorController(ILogger<DoctorController> logger, IConsultationService consultationService, IChildService childService) : base(logger)
    {
        _consultationService = consultationService;
        _childService = childService;
    }
    
    [HttpGet(ApiEndPointConstant.Doctor.ConsultationDoctor)]
    [ProducesResponseType(typeof(IPaginate<ConsultationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsultationByDoctorId([FromQuery]int page = 1,[FromQuery] int size = 30,int doctorId = 0)
    {
        var consultations = await _consultationService.GetConsultationByDoctorIdAsync(page, size, doctorId);
        return Ok(consultations);
    }

    [HttpGet(ApiEndPointConstant.Doctor.GetChildProfile)]
    [ProducesResponseType(typeof(IPaginate<ChildResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChildProfile(int id)
    {
        var doctorId = User.FindFirstValue("userId");
        var doctorIdInt = int.Parse(doctorId);
        var response = await _childService.GetChildByIdForDoctorAsync(doctorIdInt, id);
        return Ok(response);
    }
    [HttpPatch(ApiEndPointConstant.Doctor.ApproveConsultation)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    [CustomAuthorize(RoleEnum.Doctor)]
    public async Task<IActionResult> ApproveConsultation(int consultationId)
    {
        var doctorId = User.FindFirstValue("userId");
        var doctorIdInt = int.Parse(doctorId);
        var response = await _consultationService.ApproveConsultationAsync(doctorIdInt, consultationId);
        return Ok(response);
    }
    [HttpPatch(ApiEndPointConstant.Doctor.RequestChildGrowthRecord)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    [CustomAuthorize(RoleEnum.Doctor)]
    public async Task<IActionResult> RequestChildGrowthRecord(int consultationId)
    {
        var doctorId = User.FindFirstValue("userId");
        var doctorIdInt = int.Parse(doctorId);
        var response = await _consultationService.RequestChildGrowthRecordAsync(doctorIdInt, consultationId);
        return Ok(response);
    }
    [HttpGet(ApiEndPointConstant.Doctor.Dashboard)]
    [ProducesResponseType(typeof(DoctorDashboardResponse), StatusCodes.Status200OK)]
    [CustomAuthorize(RoleEnum.Doctor)]
    public async Task<IActionResult> GetDoctorDashboard([FromQuery] int month)
    {
        var doctorId = User.FindFirstValue("userId");
        var doctorIdInt = int.Parse(doctorId);
        var response = await _consultationService.GetDoctorDashboardAsync(doctorIdInt, month);
        return Ok(response);
    }
    
    
}