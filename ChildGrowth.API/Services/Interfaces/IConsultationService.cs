using ChildGrowth.API.Payload.Request.Consultation;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IConsultationService
{
    Task<IPaginate<ConsultationResponse>> GetConsultationByDoctorIdAsync(int page, int size, int doctorId);
    
    Task<ConsultationResponse> CreateConsultationAsync(int parentId, CreateConsultationRequest request);
}