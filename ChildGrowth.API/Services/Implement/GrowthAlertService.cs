using AutoMapper;
using ChildGrowth.API.Payload.Response.GrowthAlert;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChildGrowth.API.Services.Implement;

public class GrowthAlertService : BaseService<GrowthAlert>, IGrowthAlertService
{
    public GrowthAlertService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<GrowthAlert> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<GrowthAlertResponse>> GetGrowthAlertByChildIdAsync(int page, int size, int childId)
    {
        var growthAlerts = await _unitOfWork.GetRepository<GrowthAlert>().GetPagingListAsync(
            predicate: x => x.ChildId == childId,
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(y => y.CreatedAt),
            include: x => x.Include(y => y.HandledByNavigation)
        );
        return _mapper.Map<IPaginate<GrowthAlertResponse>>(growthAlerts);
    }
}