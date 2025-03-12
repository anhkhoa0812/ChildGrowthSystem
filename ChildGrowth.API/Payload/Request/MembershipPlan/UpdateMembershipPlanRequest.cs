using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.MembershipPlan;

public class UpdateMembershipPlanRequest
{
    [Required]
    public int PlanId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 month")]
    public int? Duration { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "MaxChildren must be at least 1")]
    public int? MaxChildren { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "ConsultationLimit must be at least 1")]
    public int? ConsultationLimit { get; set; }

    public string? Features { get; set; }

    [Range(0, 100, ErrorMessage = "DiscountPercentage must be between 0 and 100")]
    public decimal? DiscountPercentage { get; set; }

    [Range(0, 100, ErrorMessage = "AnnualDiscount must be between 0 and 100")]
    public decimal? AnnualDiscount { get; set; }

    public bool? PrioritySupport { get; set; }
}