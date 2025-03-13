using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.Consultation;

public class CreateConsultationRequest
{
    [Required]
    public string Description { get; set; }
    [Required]
    public string ConsultationType { get; set; }
}