using AutoMapper;
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
}