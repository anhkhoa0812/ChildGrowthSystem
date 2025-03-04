using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

[Route(ApiEndPointConstant.Doctor.DoctorEndPoint)]
[ApiController]
public class DoctorController : BaseController<DoctorController>
{
    private readonly IConsultationService _consultationService;
    public DoctorController(ILogger<DoctorController> logger, IConsultationService consultationService) : base(logger)
    {
        _consultationService = consultationService;
    }
    
    [HttpGet(ApiEndPointConstant.Doctor.ConsultationDoctor)]
    [ProducesResponseType(typeof(IPaginate<ConsultationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsultationByDoctorId([FromQuery]int page = 1,[FromQuery] int size = 30,int doctorId = 0)
    {
        var consultations = await _consultationService.GetConsultationByDoctorIdAsync(page, size, doctorId);
        return Ok(consultations);
    }
}