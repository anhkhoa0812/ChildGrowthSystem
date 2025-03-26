using AutoMapper;
using ChildGrowth.API.Payload.Request.Consultation;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.API.Payload.Response.Doctor;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Enum;
using ChildGrowth.Domain.Filter.ModelFilter;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChildGrowth.API.Services.Implement;

public class ConsultationService : BaseService<ConsultationService>, IConsultationService
{
    public ConsultationService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<ConsultationService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<ConsultationResponse>> GetConsultationByDoctorIdAsync(int page, int size, int doctorId,  ConsultationFilter? filter, string? sortBy, bool isAsc)
    {
        var consultation = await _unitOfWork.GetRepository<Consultation>().GetPagingListAsync(
            predicate: x => x.DoctorId == doctorId,
            page: page,
            size: size,
            sortBy: sortBy ?? "FollowUpDate",
            filter: filter,
            isAsc: isAsc,
            include: x => x.Include(x => x.Parent)
                .Include(x => x.Child)
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
            Status = EConsultationStatus.Pending.ToString(),
            FollowUpDate = DateTime.Now
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
        consultation.FollowUpDate = DateTime.Now;
        consultation.Status = EConsultationStatus.Completed.ToString();
        _unitOfWork.GetRepository<Consultation>().UpdateAsync(consultation);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ConsultationResponse>(consultation);
    }

    public async Task<IPaginate<FeedbackConsultationResponse>> GetFeedbackConsultations()
    {
        var consultations = await _unitOfWork.GetRepository<Consultation>().GetPagingListAsync(
            predicate: x => x.Feedback != null,
            orderBy: x => x.OrderByDescending(x => x.RequestDate),
            include: x => x.Include(x => x.Doctor)
                .Include(x => x.Parent)
        );
        return _mapper.Map<IPaginate<FeedbackConsultationResponse>>(consultations);
        
    }

    public async Task<ConsultationResponse> ApproveConsultationAsync(int doctorId, int consultationId)
    {
        var consultation = await _unitOfWork.GetRepository<Consultation>().SingleOrDefaultAsync(
            predicate: x => x.ConsultationId == consultationId
        );
        if (consultation == null)
        {
            throw new BadHttpRequestException("Can not find consultation");
        }
        if (consultation.DoctorId != null)
        {
            throw new BadHttpRequestException("This consultation is already approved");
        }

        if (consultation.Status != EConsultationStatus.Pending.ToString())
        {
            throw new BadHttpRequestException("This consultation is not pending");
        }
        
        consultation.DoctorId = doctorId;
        consultation.Status = EConsultationStatus.Approved.ToString();
        consultation.FollowUpDate = DateTime.Now;
        _unitOfWork.GetRepository<Consultation>().UpdateAsync(consultation);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ConsultationResponse>(consultation);
        
    }

    public async Task<ConsultationResponse> RequestChildGrowthRecordAsync(int doctorId, int consultationId)
    {
        var consultation = await _unitOfWork.GetRepository<Consultation>().SingleOrDefaultAsync(
            predicate: x => x.ConsultationId == consultationId && x.DoctorId == doctorId
        );
        if (consultation == null)
        {
            throw new BadHttpRequestException("Can not find consultation or you are not the doctor of this consultation");
        }
        if (consultation.Status != EConsultationStatus.Approved.ToString())
        {
            throw new BadHttpRequestException("This consultation is not approved");
        }
        consultation.Status = EConsultationStatus.SharedData.ToString();
        consultation.FollowUpDate = DateTime.Now;
        _unitOfWork.GetRepository<Consultation>().UpdateAsync(consultation);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ConsultationResponse>(consultation);
    }

    public async Task<ConsultationResponse> ShareChildGrowthRecordAsync(int parentId, SharedChildGrowthRequest request)
    {
        var consultation = await _unitOfWork.GetRepository<Consultation>().SingleOrDefaultAsync(
            predicate: x => x.ConsultationId == request.ConsultationId && x.ParentId == parentId,
            include: x => x.Include(x => x.Parent)
                .ThenInclude(x => x.Children)
        );
        if (consultation == null)
        {
            throw new BadHttpRequestException("Can not find consultation or you are not the parent of this consultation");
        }
        if (consultation.Status != EConsultationStatus.RequestSharedData.ToString())
        {
            throw new BadHttpRequestException("This consultation is not in requesting shared data");
        }

        if (!consultation.Parent.Children.Any(x => x.ChildId == request.ChildId))
        {
            throw new BadHttpRequestException("This child is not belong to this parent");
        }
        consultation.ChildId = request.ChildId;
        consultation.Status = EConsultationStatus.SharedData.ToString();
        consultation.FollowUpDate = DateTime.Now;
        _unitOfWork.GetRepository<Consultation>().UpdateAsync(consultation);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ConsultationResponse>(consultation);
    }

    public async Task<DoctorDashboardResponse> GetDoctorDashboardAsync(int doctorId, int month)
    {
        var consultations = await _unitOfWork.GetRepository<Consultation>().GetListAsync();
        var doctor = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: x => x.UserId == doctorId
        );
        if (doctor == null)
        {
            throw new BadHttpRequestException("Can not find doctor");
        }
        var doctorDashboardResponse = new DoctorDashboardResponse();
        doctorDashboardResponse.PendingConsultationTodayCount =
            consultations.Count(x => x.Status == EConsultationStatus.Pending.ToString());

        var startDate = DateTime.Today.AddDays(-1);
        var endDate = DateTime.Today;
        
        var consultationsToday = consultations.Where(x => x.FollowUpDate >= startDate && x.FollowUpDate <= endDate && x.DoctorId == doctorId);
        doctorDashboardResponse.AllConsultationTodayCount = consultationsToday.Count();
        doctorDashboardResponse.CompletedConsultationTodayCount =
            consultationsToday.Count(x => x.Status == EConsultationStatus.Completed.ToString());
        doctorDashboardResponse.SharedDataConsultationTodayCount =
            consultationsToday.Count(x => x.Status == EConsultationStatus.SharedData.ToString());
        
        var doctorDashboardResponseByMonth = new DoctorDashboardResponseByMonth();
        var consultationsByMonth = consultations.Where(x => x.FollowUpDate!.Value.Month == month 
                                                            && x.FollowUpDate!.Value.Year == DateTime.UtcNow.Year 
                                                            && x.DoctorId == doctorId);
        doctorDashboardResponseByMonth.RejectedConsultationCount = consultationsByMonth.Count(x => 
            x.Status == EConsultationStatus.Rejected.ToString());
        doctorDashboardResponseByMonth.CompletedConsultationCount = consultationsByMonth.Count(x => 
            x.Status == EConsultationStatus.Completed.ToString());
        doctorDashboardResponseByMonth.CancelledConsultationCount = consultationsByMonth.Count(x => 
            x.Status == EConsultationStatus.Cancelled.ToString());
        
        doctorDashboardResponse.ByMonth = doctorDashboardResponseByMonth;

        return doctorDashboardResponse;
    }

    public async Task<IPaginate<ConsultationResponse>> GetAllPendingConsultations(int page, int size, ConsultationFilter? filter, string? sortBy, bool isAsc)
    {
        var consultations = await _unitOfWork.GetRepository<Consultation>().GetPagingListAsync(
            predicate: x => x.Status == EConsultationStatus.Pending.ToString(),
            page: page,
            size: size,
            sortBy: sortBy ?? "RequestDate",
            isAsc: isAsc,
            include: x => x.Include(x => x.Parent)
        );
        return _mapper.Map<IPaginate<ConsultationResponse>>(consultations);
    }

    public async Task<ConsultationResponse> GetPendingConsultationByIdAsync(int consultationId)
    {
        var consultation = await _unitOfWork.GetRepository<Consultation>().SingleOrDefaultAsync(
            predicate: x => x.ConsultationId == consultationId && x.Status == EConsultationStatus.Pending.ToString(),
            include: x => x.Include(x => x.Parent)
        );
        if (consultation == null)
        {
            throw new BadHttpRequestException("Can not find consultation");
        }
        return _mapper.Map<ConsultationResponse>(consultation);
    }
}