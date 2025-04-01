using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.Consultation;

public class FeedbackConsultationRequest
{
    [Required]
    public string Feedback { get; set; }
    [Required]
    public int Rating { get; set; }
}