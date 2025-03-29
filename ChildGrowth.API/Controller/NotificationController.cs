using System.Security.Claims;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Response.Notification;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

[Route(ApiEndPointConstant.Notification.NotificationEndPoint)]
[ApiController]
public class NotificationController : BaseController<NotificationController>
{
    private readonly INotificationService _notificationService;
    
    public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService) : base(logger)
    {
        _notificationService = notificationService;
    }

    [HttpGet(ApiEndPointConstant.Notification.NotificationEndPoint)]
    [ProducesResponseType(typeof(IPaginate<GetNotificationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetNotifications([FromQuery] int page = 1, [FromQuery] int size = 30)
    {
        var parentId = User.FindFirstValue("userId");
        var parentIdInt = int.Parse(parentId);
        try
        {
            var notification = await _notificationService.GetNotifications(parentIdInt, page, size);
            return Ok(notification);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
}