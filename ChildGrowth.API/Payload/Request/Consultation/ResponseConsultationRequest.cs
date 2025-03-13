using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.Consultation;

public class ResponseConsultationRequest
{
    [Required]
    public string Response { get; set; }
}