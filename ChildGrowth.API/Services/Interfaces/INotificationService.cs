using ChildGrowth.API.Payload.Response.Notification;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces;

public interface INotificationService
{
    Task<IPaginate<GetNotificationResponse>> GetNotifications(int id, int page, int size);
}
