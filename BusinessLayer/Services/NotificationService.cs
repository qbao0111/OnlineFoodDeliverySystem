using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notifications;

    public NotificationService(INotificationRepository notifications)
    {
        _notifications = notifications;
    }

    public async Task<NotificationDto> SendNotificationAsync(SendNotificationDto request, CancellationToken cancellationToken = default)
    {
        var notification = new Notification { UserId = request.UserId, Message = request.Message, Type = request.Type };
        await _notifications.AddAsync(notification, cancellationToken);
        await _notifications.SaveChangesAsync(cancellationToken);
        return ToDto(notification);
    }

    public async Task<IReadOnlyList<NotificationDto>> GetUserNotificationsAsync(int userId, CancellationToken cancellationToken = default) =>
        (await _notifications.GetByUserAsync(userId, cancellationToken)).Select(ToDto).ToList();

    private static NotificationDto ToDto(Notification notification) =>
        new(notification.Id, notification.UserId, notification.Message, notification.Type, notification.IsRead, notification.CreatedAt);
}
