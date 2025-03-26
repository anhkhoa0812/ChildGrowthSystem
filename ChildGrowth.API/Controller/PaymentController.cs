using ChildGrowth.API.Constants;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Request.Payment;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

public class PaymentController : BaseController<PaymentController>
{
    private readonly IPaymentService _paymentService;
    public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService) : base(logger)
    {
        _paymentService = paymentService;
    }
    
    [HttpPost(ApiEndPointConstant.Payment.PaymentEndPoint)]
    [CustomAuthorize(RoleEnum.Member)]
    public async Task<IActionResult> PayMembershipPlan([FromBody] PayPlanRequest request)
    {
        var result = await _paymentService.PayMembershipPlan(request);
        return Ok(result);
    }
    
    [HttpPatch(ApiEndPointConstant.Payment.PaymentEndPoint)]
    [CustomAuthorize(RoleEnum.Member)]
    public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentRequest request)
    {
        var result = await _paymentService.UpdatePayment(request);
        return Ok(result);
    }
    
}