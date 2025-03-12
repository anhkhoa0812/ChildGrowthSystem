using AutoMapper;
using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class GrowthRecordMapper : Profile
{
    public GrowthRecordMapper()
    {
        CreateMap<GrowthRecord, GrowthRecordResponse>();
    }
    
}