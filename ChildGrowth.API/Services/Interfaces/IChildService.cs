using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IChildService
{
    Task<IPaginate<ChildResponse>> GetAllChildren(int page, int size);
}