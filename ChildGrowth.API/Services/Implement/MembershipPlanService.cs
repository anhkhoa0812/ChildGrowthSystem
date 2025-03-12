using AutoMapper;
using ChildGrowth.API.Payload.Request.MembershipPlan;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Repository.Interfaces;

namespace ChildGrowth.API.Services.Implement;

public class MembershipPlanService : BaseService<MembershipPlanService>, IMembershipPlanService
{
    public MembershipPlanService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<MembershipPlanService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        
    }

    public async Task<IEnumerable<MembershipPlan>> GetMembershipPlans()
    {
        try
        {
            var membershipPlans = await _unitOfWork.GetRepository<MembershipPlan>().GetListAsync();
            return membershipPlans;
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<MembershipPlan> GetMembershipPlanById(int id)
    {
        var result = _unitOfWork.GetRepository<MembershipPlan>().SingleOrDefaultAsync(
                predicate: m => m.PlanId == id);
        return result;
    }

    public async Task<MembershipPlan> CreateMembershipPlan(CreateMembershipPlanRequest request)
    {
        try
        {
            var membershipPlan = _mapper.Map<MembershipPlan>(request);
            await _unitOfWork.GetRepository<MembershipPlan>().InsertAsync(membershipPlan);
            await _unitOfWork.CommitAsync();
            return membershipPlan;
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<MembershipPlan> UpdateMembershipPlan(UpdateMembershipPlanRequest request)
    {
        var membershipPlan = await _unitOfWork.GetRepository<MembershipPlan>().SingleOrDefaultAsync(
            predicate: m => m.PlanId == request.PlanId);
        if (membershipPlan == null)
            throw new Exception("Membership plan not found");
        var updatedMembershipPlan = _mapper.Map<MembershipPlan>(request);
        _unitOfWork.GetRepository<MembershipPlan>().UpdateAsync(updatedMembershipPlan);
        await _unitOfWork.CommitAsync();
        return updatedMembershipPlan;
    }

    public async Task<bool> InactiveMembershipPlan(int id)
    {
        var membershipPlan = await _unitOfWork.GetRepository<MembershipPlan>().SingleOrDefaultAsync(
            predicate: m => m.PlanId == id);
        if (membershipPlan == null)
            throw new Exception("Membership plan not found");
        membershipPlan.Status = "Inactive";
        _unitOfWork.GetRepository<MembershipPlan>().UpdateAsync(membershipPlan);
        _unitOfWork.CommitAsync();
        return true;
    }
}