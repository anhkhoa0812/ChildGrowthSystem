using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class MembershipPlan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? Duration { get; set; }

    public int? MaxChildren { get; set; }

    public int? ConsultationLimit { get; set; }

    public string? Features { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? AnnualDiscount { get; set; }

    public bool? PrioritySupport { get; set; }

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
