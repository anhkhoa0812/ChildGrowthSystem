using AutoMapper;
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
}