using AutoMapper;
using ChildGrowth.API.Payload.Response.Children;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class ChildMapper : Profile
{
    public ChildMapper()
    {
        CreateMap<Child, ChildResponse>();
    }
}