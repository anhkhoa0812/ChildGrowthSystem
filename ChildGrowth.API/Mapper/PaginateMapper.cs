using AutoMapper;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Mapper;

public class PaginateMapper : Profile
{
    public PaginateMapper()
    {
        CreateMap(typeof(IPaginate<>), typeof(IPaginate<>)).ConvertUsing(typeof(PaginateConverter<,>));
    }
}