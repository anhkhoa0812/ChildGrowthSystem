using AutoMapper;
using ChildGrowth.API.Payload.Response.Consultation;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class ConsultationMapper : Profile
{
    public ConsultationMapper()
    {
        CreateMap<Consultation, ConsultationResponse>();
    }
}