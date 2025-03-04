using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class GrowthAlert
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

    public virtual Child? Child { get; set; }

    public virtual User? HandledByNavigation { get; set; }
}
