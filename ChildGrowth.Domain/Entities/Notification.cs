using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public string? Type { get; set; }

    public int? RelatedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public bool? IsRead { get; set; }

    public string? Priority { get; set; }

    public virtual User? User { get; set; }
}
