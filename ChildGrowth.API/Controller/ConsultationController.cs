using System.Security.Claims;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Request.Consultation;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Validators;
using ChildGrowth.Domain.Enum;
using ChildGrowth.Domain.Filter.ModelFilter;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

[Route(ApiEndPointConstant.Consultation.ConsultationEndpoint)]
[ApiController]
public class ConsultationController : BaseController<ConsultationController>
{
    private readonly IConsultationService _consultationService;
    public ConsultationController(ILogger<ConsultationController> logger, IConsultationService consultationService) : base(logger)
    {
        _consultationService = consultationService;
    }
    
    [HttpPost(ApiEndPointConstant.Consultation.ConsultationEndpoint)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateConsultation([FromBody] CreateConsultationRequest request)
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId);
        try
        {
            var consultation = await _consultationService.CreateConsultationAsync(parentIdInt, request);
            return CreatedAtAction(nameof(CreateConsultation), consultation);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPut(ApiEndPointConstant.Consultation.ResponseConsultation)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResponseConsultation(int id, ResponseConsultationRequest request)
    {
        var doctorId = User.FindFirstValue("userId");
        var doctorIdInt = int.Parse(doctorId);
        try
        {
            var consultation = await _consultationService.ResponseConsultationAsync(doctorIdInt, id, request);
            return Ok(consultation);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet(ApiEndPointConstant.Consultation.FeedbackConsultation)]
    [CustomAuthorize(RoleEnum.Admin)] 
    [ProducesResponseType(typeof(IPaginate<FeedbackConsultationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFeedbackConsultations([FromQuery] int page = 1, [FromQuery] int size = 30)
    {
        try
        {
            var consultations = await _consultationService.GetFeedbackConsultations();
            return Ok(consultations);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
    [CustomAuthorize(RoleEnum.Member)]
    [HttpPatch(ApiEndPointConstant.Consultation.SharedData)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ShareChildGrowthRecord(int id, [FromBody] SharedChildGrowthRequest request)
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId!);
        try
        {
            var consultation = await _consultationService.ShareChildGrowthRecordAsync(parentIdInt, id, request);
            return Ok(consultation);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
    [CustomAuthorize(RoleEnum.Doctor, RoleEnum.Admin)]
    [HttpGet(ApiEndPointConstant.Consultation.Pending)]
    [ProducesResponseType(typeof(IPaginate<ConsultationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllPendingConsultations([FromQuery] int page = 1, 
        [FromQuery] int size = 30, 
        [FromQuery] ConsultationFilter? filter = null, 
        [FromQuery] string? sortBy = null, 
        [FromQuery] bool isAsc = false)
    {
        try
        {
            var consultations = await _consultationService.GetAllPendingConsultations(page, size, filter, sortBy, isAsc);
            return Ok(consultations);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Doctor)]
    [HttpGet(ApiEndPointConstant.Consultation.PendingById)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPendingConsultationById(int id)
    {
        try
        {
            var consultation = await _consultationService.GetPendingConsultationByIdAsync(id);
            return Ok(consultation);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
    [HttpPatch(ApiEndPointConstant.Consultation.FeedbackConsultationById)]
    [CustomAuthorize(RoleEnum.Member)]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FeedbackConsultationsByParent(int id, [FromBody] FeedbackConsultationRequest request)
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId);
        try
        {
            var consultation = await _consultationService.FeedbackConsultationsByParent(id, parentIdInt, request);
            return Ok(consultation);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
}
