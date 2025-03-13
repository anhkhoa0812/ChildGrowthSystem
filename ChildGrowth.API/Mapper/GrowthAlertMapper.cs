using AutoMapper;
using ChildGrowth.API.Payload.Response.GrowthAlert;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class GrowthAlertMapper : Profile
{
    public GrowthAlertMapper()
    {
        CreateMap<GrowthAlert, GrowthAlertResponse>()
            .ForMember(dest => dest.HandledDoctor, opt => opt.MapFrom(src => src.HandledByNavigation));
    }
    
}