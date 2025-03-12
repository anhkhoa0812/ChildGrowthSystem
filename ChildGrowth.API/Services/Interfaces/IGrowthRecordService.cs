using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IGrowthRecordService
{
    Task<IPaginate<GrowthRecordResponse>> GetGrowthRecordByChildIdAsync(int page, int size, int childId);
}