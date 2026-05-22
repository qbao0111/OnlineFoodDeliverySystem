using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface IOrderService
{
    Task<ApiResponse<OrderDto>> PlaceOrderAsync(PlaceOrderDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<OrderDto>> CancelOrderAsync(int orderId, CancellationToken cancellationToken = default);
    Task<ApiResponse<OrderDto>> TrackOrderAsync(int orderId, CancellationToken cancellationToken = default);
    Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(int orderId, string status, CancellationToken cancellationToken = default);
}
