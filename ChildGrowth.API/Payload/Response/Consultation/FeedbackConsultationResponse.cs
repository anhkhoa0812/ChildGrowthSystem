using ChildGrowth.API.Payload.Response.User;

namespace ChildGrowth.API.Payload.Response.Consultation;

public class FeedbackConsultationResponse
{
    public int ConsultationId { get; set; }
    public string? Feedback { get; set; }
    public int? Rating { get; set; }
    public string? Status { get; set; }
    public UserResponse? Doctor { get; set; }
    public UserResponse? Parent { get; set; }
}