using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface INotificationService
{
    Task<NotificationDto> SendNotificationAsync(SendNotificationDto request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<NotificationDto>> GetUserNotificationsAsync(int userId, CancellationToken cancellationToken = default);
}
