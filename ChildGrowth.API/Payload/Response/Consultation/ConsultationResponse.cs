using ChildGrowth.API.Payload.Response.User;

namespace ChildGrowth.API.Payload.Response.Consultation;

public class ConsultationResponse
{
    public int ConsultationId { get; set; }

    public int? ParentId { get; set; }

    public int? DoctorId { get; set; }

    public int? ChildId { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? ConsultationType { get; set; }

    public string? Description { get; set; }

    public string? SharedData { get; set; }

    public string? Status { get; set; }

    public string? Response { get; set; }

    public DateTime? ResponseDate { get; set; }

    public int? Rating { get; set; }

    public string? Feedback { get; set; }

    public DateTime? FollowUpDate { get; set; }

    public string? Priority { get; set; }
    public UserResponse? Parent { get; set; }
}