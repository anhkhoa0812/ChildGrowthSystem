using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Services.Interfaces;

public interface IMembershipPlanService
{
    Task<IEnumerable<MembershipPlan>> GetMembershipPlans();
}