using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notifications;

    public NotificationsController(INotificationService notifications)
    {
        _notifications = notifications;
    }

    [HttpGet("users/{userId:int}")]
    public async Task<IActionResult> GetUserNotifications(int userId, CancellationToken cancellationToken) =>
        Ok(await _notifications.GetUserNotificationsAsync(userId, cancellationToken));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> SendNotification(SendNotificationDto request, CancellationToken cancellationToken) =>
        Ok(await _notifications.SendNotificationAsync(request, cancellationToken));
}
