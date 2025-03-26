namespace ChildGrowth.API.Payload.Request.Payment;

public class PayPlanRequest
{
    public int UserId { get; set; }
    public int MembershipPlanId { get; set; }
    public bool AutoRenew { get; set; }
}