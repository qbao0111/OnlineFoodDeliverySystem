namespace BusinessLayer.DTOs;

public record DeliveryDto(int Id, int OrderId, int? DriverId, string DeliveryStatus, DateTime? PickupTime, DateTime? DeliveredTime);
public record AcceptDeliveryDto(int DeliveryId, int DriverId);
public record UpdateDeliveryStatusDto(string Status);
