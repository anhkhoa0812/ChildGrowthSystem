using AutoMapper;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Request.Payment;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;

namespace ChildGrowth.API.Services.Implement;

public class PaymentService : BaseService<PaymentService>, IPaymentService
{
    private readonly PayOsSettings _payOsSettings;
    public PaymentService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<PaymentService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOptions<PayOsSettings> payOsSettings) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _payOsSettings = payOsSettings.Value;
    }

    public async Task<string> PayMembershipPlan(PayPlanRequest request)
    {
        try
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.UserId == request.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var membershipPlan = await _unitOfWork.GetRepository<MembershipPlan>().SingleOrDefaultAsync(predicate: mp => mp.PlanId == request.MembershipPlanId);
        if (membershipPlan == null)
        {
            throw new Exception("Membership plan not found");
        }
        PayOS payOs = new PayOS(_payOsSettings.ClientId, _payOsSettings.ApiKey, _payOsSettings.ChecksumKey);
        long orderCode = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        string description = "Thanh toán gói thành viên";
        var items = new List<ItemData>();
        var totalAmount = (int)membershipPlan.Price;
        items.Add(new ItemData(
            membershipPlan.PlanName, 
            1,
            (int)membershipPlan.Price
        ));
        var userMembership = new UserMembership
        {
            UserId = request.UserId,
            PlanId = request.MembershipPlanId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(membershipPlan.Duration??1),
            PaymentAmount = membershipPlan.Price,
            PaymentStatus = PaymentStatusEnum.Pending.ToString(),
            PaymentMethod = "Banking",
            OrderCode = orderCode,
            AutoRenewal = request.AutoRenew,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")),
            Status = PaymentStatusEnum.Pending.ToString()
        };
        await _unitOfWork.GetRepository<UserMembership>().InsertAsync(userMembership);
        await _unitOfWork.CommitAsync();
        PaymentData paymentData = new PaymentData(
            orderCode, 
            totalAmount, 
            description, 
            items, 
            _payOsSettings.CancelUrl, 
            _payOsSettings.ReturnUrl,
            buyerName: user.FullName, 
            buyerEmail: user.Email,
            buyerPhone: user.PhoneNumber,
            expiredAt: ((DateTimeOffset) TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")).AddMinutes(10)).ToUnixTimeSeconds()
        );
        CreatePaymentResult createPayment = await payOs.createPaymentLink(paymentData);
        return createPayment.checkoutUrl;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
            return null;
        }
    }

    public async Task<bool> UpdatePayment(UpdatePaymentRequest request)
    {
        try
        {
            var userMembership = await _unitOfWork.GetRepository<UserMembership>().SingleOrDefaultAsync(predicate: um => um.OrderCode == request.OrderCode && um.UserId == request.UserId);
            if(userMembership == null || userMembership.Status == PaymentStatusEnum.Paid.ToString() || userMembership.Status == PaymentStatusEnum.Cancelled.ToString())
            {
                throw new Exception("User membership cancelled or already paid");
            }

            PayOS payOs = new PayOS(_payOsSettings.ClientId, _payOsSettings.ApiKey, _payOsSettings.ChecksumKey);
            PaymentLinkInformation paymentLinkInformation = await payOs.getPaymentLinkInformation(request.OrderCode);
            if(paymentLinkInformation == null)
            {
                throw new Exception("Payment not found");
            }
            if(paymentLinkInformation.status == "Pending" || paymentLinkInformation.status == "Processing")
            {
                return false;
            }
            userMembership.Status = paymentLinkInformation.status;
            _unitOfWork.GetRepository<UserMembership>().UpdateAsync(userMembership);
            await _unitOfWork.CommitAsync();
            return true;
        } catch (Exception e)
        {
            throw new Exception(e.Message);
            return false;
        }
    }
}