using ChildGrowth.API.Payload.Response.User;

namespace ChildGrowth.API.Payload.Response.GrowthAlert;

public class GrowthAlertResponse
{
    public int AlertId { get; set; }

    public int? ChildId { get; set; }

    public string? AlertType { get; set; }

    public string? AlertLevel { get; set; }

    public string? Description { get; set; }

    public string? RecommendedActions { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? ReadStatus { get; set; }

    public bool? HandledStatus { get; set; }

    public int? HandledBy { get; set; }

    public DateTime? HandledAt { get; set; }

    public string? Notes { get; set; }
    
    public UserResponse HandledDoctor { get; set; } = null!;
}