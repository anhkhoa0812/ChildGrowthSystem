namespace ChildGrowth.API.Payload.Response.Notification;

public class GetNotificationResponse
{
    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public string? Type { get; set; }

    public int? RelatedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public bool? IsRead { get; set; }

    public string? Priority { get; set; }
}