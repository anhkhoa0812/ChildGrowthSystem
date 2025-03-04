using ChildGrowth.API.Constants;
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
    
}
