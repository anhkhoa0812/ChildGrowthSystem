using AutoMapper;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Request.Consultation;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;

namespace ChildGrowth.API.Services.Implement;

public class ConsultationService : BaseService<ConsultationService>, IConsultationService
{
    public ConsultationService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<ConsultationService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<ConsultationResponse>> GetConsultationByDoctorIdAsync(int page, int size, int doctorId)
    {
        var consultation = await _unitOfWork.GetRepository<Consultation>().GetPagingListAsync(
            predicate: x => x.DoctorId == doctorId,
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(x => x.RequestDate)
        );
        return _mapper.Map<IPaginate<ConsultationResponse>>(consultation);
    }

    public async Task<ConsultationResponse> CreateConsultationAsync(int parentId, CreateConsultationRequest request)
    {
        var parent = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: x => x.UserId == parentId && x.UserType == RoleEnum.Member.ToString()
        );
        if (parent == null)
        {
            throw new BadHttpRequestException("Can not find parent");
        }
        var consultationType = parent.MembershipStatus == "Prenium" ? EConsultationType.Premium.ToString() : EConsultationType.Basic.ToString();
        var consultation = new Consultation()
        {
            ParentId = parentId,
            Description = request.Description,
            RequestDate = DateTime.UtcNow,
            Priority = "Normal",
            ConsultationType = consultationType,
            Status = EConsultationStatus.Pending.ToString()
        };
        await _unitOfWork.GetRepository<Consultation>().InsertAsync(consultation);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ConsultationResponse>(consultation);
    }

    public async Task<ConsultationResponse> ResponseConsultationAsync(int doctorId, int consultationId, ResponseConsultationRequest request)
    {
        var parent = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: x => x.UserId == doctorId && x.UserType == RoleEnum.Doctor.ToString()
        );
        if(parent == null)
            throw new BadHttpRequestException("Can not find doctor");
        var consultation = await _unitOfWork.GetRepository<Consultation>().SingleOrDefaultAsync(
            predicate:x => x.ConsultationId == consultationId && x.DoctorId == doctorId
        );
        if (consultation == null)
            throw new BadHttpRequestException("Can not find consultation or you are not the doctor of this consultation");

        if(consultation.Status != EConsultationStatus.SharedData.ToString())
            throw new BadHttpRequestException("Can not response to this consultation");
        consultation.Response = request.Response;
        consultation.ResponseDate = DateTime.UtcNow;
        consultation.Status = EConsultationStatus.Completed.ToString();
        _unitOfWork.GetRepository<Consultation>().UpdateAsync(consultation);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ConsultationResponse>(consultation);
    }
}