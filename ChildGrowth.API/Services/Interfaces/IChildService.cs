using ChildGrowth.API.Payload.Request.Children;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IChildService
{
    Task<IPaginate<ChildResponse>> GetAllChildren(int page, int size);

    Task<ChildResponse> GetChildByIdAsync(int childId);

    Task<ChildResponse> GetChildByIdForDoctorAsync(int doctorId, int consultationId);

    Task<ChildResponse> CreateChildAsync(CreateChildrenRequest request);

    Task<ChildResponse> UpdateChildAsync(int childId, UpdateChildrenRequest request);

    Task<bool> DeleteChildAsync(int childId);

}