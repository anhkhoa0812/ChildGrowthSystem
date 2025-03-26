using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.Payment;

public class PayPlanRequest
{
    [Required(ErrorMessage = "UserId is required")]
    public int UserId { get; set; }
    [Required(ErrorMessage = "MembershipPlanId is required")]
    public int MembershipPlanId { get; set; }
    [Required(ErrorMessage = "AutoRenew is required")]
    public bool AutoRenew { get; set; }
}