using AutoMapper;
using ChildGrowth.API.Payload.Request.Children;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Enum;
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
            include: c => c.Include(x => x.Consultations)
                .Include(x => x.GrowthRecords)
        );
        return _mapper.Map<IPaginate<ChildResponse>>(children);
    }
    public async Task<ChildResponse> GetChildByIdAsync(int childId)
    {
        var child = await _unitOfWork.GetRepository<Child>().SingleOrDefaultAsync(
            predicate: x => x.ChildId == childId,
            include: x => x.Include(c => c.Consultations)
                           .Include(c => c.GrowthRecords)
        );

        if (child == null) throw new KeyNotFoundException("Child not found");

        return _mapper.Map<ChildResponse>(child);
    }
    public async Task<ChildResponse> CreateChildAsync(CreateChildrenRequest request)
    {
        var child = _mapper.Map<Child>(request);

        await _unitOfWork.GetRepository<Child>().InsertAsync(child);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ChildResponse>(child); 
    }
    public async Task<ChildResponse> UpdateChildAsync(int childId, UpdateChildrenRequest request)
    {
        var child = await _unitOfWork.GetRepository<Child>().SingleOrDefaultAsync(predicate: x => x.ChildId == childId);

        _mapper.Map(request, child); 
        _unitOfWork.GetRepository<Child>().UpdateAsync(child);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ChildResponse>(child);   
    }
    public async Task<bool> DeleteChildAsync(int childId)
    {
        var child = await _unitOfWork.GetRepository<Child>().SingleOrDefaultAsync(predicate: x => x.ChildId == childId);


        _unitOfWork.GetRepository<Child>().DeleteAsync(child);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<List<ChildResponse>> GetChildrenByParentIdAsync(int parentId)
    {
        var childs = await _unitOfWork.GetRepository<Child>().GetListAsync(
            predicate: x => x.ParentId == parentId
        );
        return _mapper.Map<List<ChildResponse>>(childs);
    }

    public async Task<ChildResponse> GetChildByIdForDoctorAsync(int doctorId, int consultationId)
    {
        var doctor = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: x => x.UserId == doctorId && x.UserType == RoleEnum.Doctor.ToString()
        );
        if (doctor == null)
        {
            throw new BadHttpRequestException("Can not find doctor");
        }
        var consultation = await _unitOfWork.GetRepository<Consultation>().SingleOrDefaultAsync(
            predicate: x => x.DoctorId == doctorId && x.ConsultationId == consultationId,
            include: x => x.Include(x => x.Parent)
                .Include(x => x.Child)
                .ThenInclude(x => x.GrowthRecords.OrderByDescending(x => x.CreatedAt))
        );
        if (consultation == null)
        {
            throw new BadHttpRequestException("Can not find consultation");
        }
        
        if (consultation.ChildId == null)
        {
            throw new BadHttpRequestException("Can not find child");
        }
        var result = _mapper.Map<ChildResponse>(consultation.Child);
        if (consultation.Parent.MembershipStatus != "Premium")
        {
            result.GrowthRecords = result.GrowthRecords!.Take(1).ToList();
        }
        return result;
    }
}