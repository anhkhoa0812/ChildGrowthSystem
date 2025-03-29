using AutoMapper;
using ChildGrowth.API.Payload.Response.Notification;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChildGrowth.API.Services.Implement;

public class NotificationService: BaseService<Notification>, INotificationService
{
    public NotificationService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<Notification> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<GetNotificationResponse>> GetNotifications(int id,int page, int size)
    {
        if (id == null) throw new BadHttpRequestException("Can not find user");
        var notifications = await _unitOfWork.GetRepository<Notification>().GetPagingListAsync(
            page: page,
            size: size,
            predicate: n => n.UserId == id,
            include: n => n.Include(n => n.User)
        );
        
        return _mapper.Map<IPaginate<GetNotificationResponse>>(notifications);
    }
}