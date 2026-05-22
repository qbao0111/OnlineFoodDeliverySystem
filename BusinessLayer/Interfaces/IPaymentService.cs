using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface IPaymentService
{
    Task<ApiResponse<PaymentDto>> ProcessPaymentAsync(PaymentRequestDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<PaymentDto>> RefundPaymentAsync(int orderId, CancellationToken cancellationToken = default);
}
