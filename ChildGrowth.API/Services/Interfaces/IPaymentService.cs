using ChildGrowth.API.Payload.Request.Payment;

namespace ChildGrowth.API.Services.Interfaces;

public interface IPaymentService
{
    Task<string> PayMembershipPlan(PayPlanRequest request);
    Task<bool> UpdatePayment(UpdatePaymentRequest request);
}