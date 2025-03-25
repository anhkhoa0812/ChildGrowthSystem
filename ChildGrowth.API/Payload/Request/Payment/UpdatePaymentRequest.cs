namespace ChildGrowth.API.Payload.Request.Payment;

public class UpdatePaymentRequest
{
    public int UserId { get; set; }
    public long OrderCode { get; set; }
}