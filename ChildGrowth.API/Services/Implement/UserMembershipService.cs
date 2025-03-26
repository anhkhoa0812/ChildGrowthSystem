using AutoMapper;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Repository.Interfaces;

namespace ChildGrowth.API.Services.Implement;

public class UserMembershipService : BaseService<UserMembershipService>, IUserMembershipService
{
    public UserMembershipService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<UserMembershipService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        
    }

    public async Task<IEnumerable<UserMembership>> GetUserMemberships()
    {
        try
        {
            var result = await _unitOfWork.GetRepository<UserMembership>().GetListAsync(predicate: u => u.Status != PaymentStatusEnum.Pending.ToString());
            return result;
        } catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<UserMembership> GetUserMembershipById(int id)
    {
        try
        {
            var userMembership = await _unitOfWork.GetRepository<UserMembership>().SingleOrDefaultAsync(predicate: u => u.MembershipId == id);
            return userMembership;
        } catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}