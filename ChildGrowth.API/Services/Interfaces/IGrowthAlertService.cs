using ChildGrowth.API.Payload.Response.GrowthAlert;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IGrowthAlertService
{
    Task<IPaginate<GrowthAlertResponse>> GetGrowthAlertByChildIdAsync(int page, int size, int childId);
}