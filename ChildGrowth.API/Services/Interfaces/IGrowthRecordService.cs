using ChildGrowth.API.Payload.Request.GrowthRecord;
using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.Domain.Enum;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IGrowthRecordService
{
    Task<GrowthRecordResponse> CreateGrowthRecordAsync(CreateGrowthRecordRequest request);
    Task<GrowthRecordResponse> GetGrowthRecordByIdAsync(int recordId);
    Task<GrowthRecordResponse> UpdateGrowthRecordAsync(int recordId, UpdateGrowthRecordRequest request);
    Task<bool> DeleteGrowthRecordAsync(int recordId);
    Task<IPaginate<GrowthRecordResponse>> GetGrowthRecordByChildIdAsync(int page, int size, int childId);
    Task<GrowthRecordDataChartResponse> GetGrowthRecordDataChartByChildIdAsync(int childId, EGrowthRecordMode mode);
}