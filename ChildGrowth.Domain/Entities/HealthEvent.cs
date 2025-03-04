using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class HealthEvent
{
    public int EventId { get; set; }

    public int? ChildId { get; set; }

    public string? EventType { get; set; }

    public DateOnly? EventDate { get; set; }

    public string? Description { get; set; }

    public string? Treatment { get; set; }

    public string? Doctor { get; set; }

    public string? Hospital { get; set; }

    public string? Medications { get; set; }

    public int? Duration { get; set; }

    public string? Severity { get; set; }

    public virtual Child? Child { get; set; }
}
