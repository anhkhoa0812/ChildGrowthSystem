using System.Security.Claims;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Request.Consultation;
using ChildGrowth.API.Services.Interfaces;
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
    public async Task<IActionResult> CreateConsultation([FromBody] CreateConsultationRequest request)
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId);
        try
        {
            var consultation = await _consultationService.CreateConsultationAsync(parentIdInt, request);
            return Ok(consultation);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPut(ApiEndPointConstant.Consultation.ResponseConsultation)]
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
}
