using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.Payment;

public class UpdatePaymentRequest
{
    [Required(ErrorMessage = "UserId is required")]
    public int UserId { get; set; }
    [Required(ErrorMessage = "OrderCode is required")]
    public long OrderCode { get; set; }
}