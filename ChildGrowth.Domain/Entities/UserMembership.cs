using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class UserMembership
{
    public int MembershipId { get; set; }

    public int? UserId { get; set; }

    public int? PlanId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? PaymentAmount { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    public bool? AutoRenewal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public string? CancellationReason { get; set; }

    public virtual MembershipPlan? Plan { get; set; }

    public virtual User? User { get; set; }
}
