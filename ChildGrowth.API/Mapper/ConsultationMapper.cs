using AutoMapper;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class ConsultationMapper : Profile
{
    public ConsultationMapper()
    {
        CreateMap<Consultation, ConsultationResponse>()
            .ForMember(x => x.Parent, opt => opt.MapFrom(src => src.Parent));

        CreateMap<Consultation, FeedbackConsultationResponse>()
            .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
            .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor));
    }
}