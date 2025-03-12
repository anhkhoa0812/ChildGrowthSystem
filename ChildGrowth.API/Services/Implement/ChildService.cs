using AutoMapper;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChildGrowth.API.Services.Implement;

public class ChildService  : BaseService<Child>, IChildService
{
    public ChildService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<Child> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<ChildResponse>> GetAllChildren(int page, int size)
    {
        var children = await _unitOfWork.GetRepository<Child>().GetPagingListAsync(
            page: page,
            size: size,
            include: c => c.Include(x => x.Consultations).Include(x => x.GrowthRecords)
        );
        return _mapper.Map<IPaginate<ChildResponse>>(children);
    }
}