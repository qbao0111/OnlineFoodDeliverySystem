using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _deliveries;

    public DeliveryService(IDeliveryRepository deliveries)
    {
        _deliveries = deliveries;
    }

    public async Task<IReadOnlyList<DeliveryDto>> ViewTasksAsync(CancellationToken cancellationToken = default) =>
        (await _deliveries.GetAvailableTasksAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<ApiResponse<DeliveryDto>> AcceptTaskAsync(AcceptDeliveryDto request, CancellationToken cancellationToken = default)
    {
        var delivery = await _deliveries.GetByIdAsync(request.DeliveryId, cancellationToken);
        if (delivery is null) return ApiResponse<DeliveryDto>.Fail("Delivery task was not found.");

        delivery.DriverId = request.DriverId;
        delivery.DeliveryStatus = "DriverAccepted";
        _deliveries.Update(delivery);
        await _deliveries.SaveChangesAsync(cancellationToken);
        return ApiResponse<DeliveryDto>.Ok(ToDto(delivery), "Delivery task accepted.");
    }

    public async Task<ApiResponse<DeliveryDto>> UpdateStatusAsync(int deliveryId, string status, CancellationToken cancellationToken = default)
    {
        var delivery = await _deliveries.GetByIdAsync(deliveryId, cancellationToken);
        if (delivery is null) return ApiResponse<DeliveryDto>.Fail("Delivery task was not found.");

        delivery.DeliveryStatus = status;
        if (status.Equals("PickedUp", StringComparison.OrdinalIgnoreCase)) delivery.PickupTime = DateTime.UtcNow;
        if (status.Equals("Delivered", StringComparison.OrdinalIgnoreCase)) delivery.DeliveredTime = DateTime.UtcNow;
        _deliveries.Update(delivery);
        await _deliveries.SaveChangesAsync(cancellationToken);
        return ApiResponse<DeliveryDto>.Ok(ToDto(delivery), "Delivery status updated.");
    }

    private static DeliveryDto ToDto(DataAccessLayer.Entities.Delivery delivery) =>
        new(delivery.Id, delivery.OrderId, delivery.DriverId, delivery.DeliveryStatus, delivery.PickupTime, delivery.DeliveredTime);
}
