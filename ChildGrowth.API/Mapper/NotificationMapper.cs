using AutoMapper;
using ChildGrowth.API.Payload.Response.Notification;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.API.Mapper;

public class NotificationMapper : Profile
{
    public NotificationMapper()
    {
        CreateMap<Notification, GetNotificationResponse>();
    }
}