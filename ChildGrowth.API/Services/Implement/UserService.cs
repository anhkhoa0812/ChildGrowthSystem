using AutoMapper;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;

namespace ChildGrowth.API.Services.Implement;

public class UserService : BaseService<User>, IUserService 
{
    public UserService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<User> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<UserResponse>> GetUserAsync(int page, int size)
    {
        var users = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(x => x.CreatedAt)
        );
        return _mapper.Map<IPaginate<UserResponse>>(users);
    }
}