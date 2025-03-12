using AutoMapper;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserResponse>();
    }
}