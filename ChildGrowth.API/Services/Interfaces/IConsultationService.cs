using ChildGrowth.API.Payload.Request.Consultation;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.API.Payload.Response.Doctor;
using ChildGrowth.Domain.Filter.ModelFilter;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface IConsultationService
{
    Task<IPaginate<ConsultationResponse>> GetConsultationByDoctorIdAsync(int page, int size, int doctorId);
    
    Task<ConsultationResponse> CreateConsultationAsync(int parentId, CreateConsultationRequest request);
    
    Task<ConsultationResponse> ResponseConsultationAsync(int doctorId, int consultationId, ResponseConsultationRequest request);
    
    Task<IPaginate<FeedbackConsultationResponse>> GetFeedbackConsultations();
    
    Task<ConsultationResponse> ApproveConsultationAsync(int doctorId, int consultationId);
    
    Task<ConsultationResponse> RequestChildGrowthRecordAsync(int doctorId, int consultationId);
    
    Task<ConsultationResponse> ShareChildGrowthRecordAsync(int parentId, SharedChildGrowthRequest request);
  
    Task<DoctorDashboardResponse> GetDoctorDashboardAsync(int doctorId, int month);
    
    Task<IPaginate<ConsultationResponse>> GetAllPendingConsultations(int page, int size, ConsultationFilter? filter, string? sortBy, bool isAsc);
}