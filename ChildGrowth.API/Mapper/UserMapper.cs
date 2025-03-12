using AutoMapper;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using Pos_System.API.Utils;

namespace ChildGrowth.API.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<SignUpRequest, User>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"));
    }
}