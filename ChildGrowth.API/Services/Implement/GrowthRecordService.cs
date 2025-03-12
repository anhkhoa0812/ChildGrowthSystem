using AutoMapper;
using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;

namespace ChildGrowth.API.Services.Implement;

public class GrowthRecordService : BaseService<GrowthRecord>, IGrowthRecordService
{
    public GrowthRecordService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<GrowthRecord> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<GrowthRecordResponse>> GetGrowthRecordByChildIdAsync(int page, int size, int childId)
    {
        var growthRecords = await _unitOfWork.GetRepository<GrowthRecord>().GetPagingListAsync(
            predicate: x => x.ChildId == childId,
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(y => y.RecordDate)
        );
        return _mapper.Map<IPaginate<GrowthRecordResponse>>(growthRecords);
    }
}