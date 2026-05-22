namespace BusinessLayer.DTOs;

public record NotificationDto(int Id, int UserId, string Message, string Type, bool IsRead, DateTime CreatedAt);
public record SendNotificationDto(int UserId, string Message, string Type);
