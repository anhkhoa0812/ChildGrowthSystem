using AutoMapper;
using ChildGrowth.API.Payload.Request.GrowthRecord;
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


    public async Task<GrowthRecordResponse> CreateGrowthRecordAsync(CreateGrowthRecordRequest request)
    {
        var growthRecord = _mapper.Map<GrowthRecord>(request);
        await _unitOfWork.GetRepository<GrowthRecord>().InsertAsync(growthRecord);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GrowthRecordResponse>(growthRecord);
    }

    public async Task<GrowthRecordResponse> GetGrowthRecordByIdAsync(int recordId)
    {
        var growthRecord = await _unitOfWork.GetRepository<GrowthRecord>()
            .SingleOrDefaultAsync(predicate: x => x.RecordId == recordId);
        if (growthRecord == null)
            throw new KeyNotFoundException("Growth record not found");
        return _mapper.Map<GrowthRecordResponse>(growthRecord);
    }

    public async Task<GrowthRecordResponse> UpdateGrowthRecordAsync(int recordId, UpdateGrowthRecordRequest request)
    {
        var growthRecord = await _unitOfWork.GetRepository<GrowthRecord>()
            .SingleOrDefaultAsync(predicate: x => x.RecordId == recordId);
        if (growthRecord == null)
            throw new KeyNotFoundException("Growth record not found");

        _mapper.Map(request, growthRecord);
        _unitOfWork.GetRepository<GrowthRecord>().UpdateAsync(growthRecord);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GrowthRecordResponse>(growthRecord);
    }
    public async Task<bool> DeleteGrowthRecordAsync(int recordId)
    {
        var growthRecord = await _unitOfWork.GetRepository<GrowthRecord>()
            .SingleOrDefaultAsync(predicate: x => x.RecordId == recordId);
        if (growthRecord == null) return false;

        _unitOfWork.GetRepository<GrowthRecord>().DeleteAsync(growthRecord);
        await _unitOfWork.CommitAsync();
        return true;
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