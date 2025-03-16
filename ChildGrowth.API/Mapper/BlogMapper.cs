using AutoMapper;
using ChildGrowth.API.Payload.Request.Blog;
using ChildGrowth.API.Payload.Response.Blog;
using ChildGrowth.Domain.Entities;
using System;

namespace ChildGrowth.API.Mapper
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Blog, BlogResponse>();

            CreateMap<CreateBlogRequest, Blog>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src =>
                    src.PublishDate.HasValue
                        ? new DateTime(src.PublishDate.Value.Year, src.PublishDate.Value.Month, src.PublishDate.Value.Day)
                        : (DateTime?)null));

            CreateMap<UpdateBlogRequest, Blog>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src =>
                    src.PublishDate.HasValue
                        ? new DateTime(src.PublishDate.Value.Year, src.PublishDate.Value.Month, src.PublishDate.Value.Day)
                        : (DateTime?)null))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
