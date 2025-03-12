using ChildGrowth.API.Payload.Request.MembershipPlan;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Services.Interfaces;

public interface IMembershipPlanService
{
    Task<IEnumerable<MembershipPlan>> GetMembershipPlans();
    Task<MembershipPlan> GetMembershipPlanById(int id);
    Task<MembershipPlan> CreateMembershipPlan(CreateMembershipPlanRequest request);
    Task<MembershipPlan> UpdateMembershipPlan(UpdateMembershipPlanRequest request);
    Task<bool> InactiveMembershipPlan(int id);
}