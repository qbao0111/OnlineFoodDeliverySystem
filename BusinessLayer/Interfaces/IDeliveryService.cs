using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface IDeliveryService
{
    Task<IReadOnlyList<DeliveryDto>> ViewTasksAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<DeliveryDto>> AcceptTaskAsync(AcceptDeliveryDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<DeliveryDto>> UpdateStatusAsync(int deliveryId, string status, CancellationToken cancellationToken = default);
}
