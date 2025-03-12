using AutoMapper;
using ChildGrowth.API.Payload.Request.MembershipPlan;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class MembershipPlanMapper : Profile
{
    public MembershipPlanMapper()
    {
        CreateMap<CreateMembershipPlanRequest, MembershipPlan>()
            .ForMember(dest => dest.PlanId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"));
        CreateMap<UpdateMembershipPlanRequest, MembershipPlan>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"));
    }
}