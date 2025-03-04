using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class FeedingRecord
{
    public int FeedingId { get; set; }

    public int? ChildId { get; set; }

    public DateTime? FeedingDate { get; set; }

    public string? FeedingType { get; set; }

    public decimal? Amount { get; set; }

    public int? Duration { get; set; }

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Child? Child { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
}
