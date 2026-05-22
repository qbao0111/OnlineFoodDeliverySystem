using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using BusinessLayer.PaymentStrategies;
using BusinessLayer.Validators;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _payments;
    private readonly IEnumerable<IPaymentStrategy> _strategies;

    public PaymentService(IPaymentRepository payments, IEnumerable<IPaymentStrategy> strategies)
    {
        _payments = payments;
        _strategies = strategies;
    }

    public async Task<ApiResponse<PaymentDto>> ProcessPaymentAsync(PaymentRequestDto request, CancellationToken cancellationToken = default)
    {
        var validation = RequestValidators.Validate(request);
        if (validation is not null) return ApiResponse<PaymentDto>.Fail(validation);

        var strategy = _strategies.FirstOrDefault(item => item.Method.Equals(request.PaymentMethod, StringComparison.OrdinalIgnoreCase));
        if (strategy is null) return ApiResponse<PaymentDto>.Fail("Unsupported payment method. Use Momo, ZaloPay, or CreditCard.");

        var paid = await strategy.PayAsync(request.Amount, cancellationToken);
        var payment = new Payment
        {
            OrderId = request.OrderId,
            Amount = request.Amount,
            PaymentMethod = strategy.Method,
            PaymentStatus = paid ? "Paid" : "Failed",
            PaidAt = paid ? DateTime.UtcNow : null
        };

        await _payments.AddAsync(payment, cancellationToken);
        await _payments.SaveChangesAsync(cancellationToken);
        return paid ? ApiResponse<PaymentDto>.Ok(ToDto(payment), "Payment successful.") : ApiResponse<PaymentDto>.Fail("Payment failed.");
    }

    public async Task<ApiResponse<PaymentDto>> RefundPaymentAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var payment = await _payments.GetByOrderAsync(orderId, cancellationToken);
        if (payment is null) return ApiResponse<PaymentDto>.Fail("Payment was not found.");

        payment.PaymentStatus = "Refunded";
        _payments.Update(payment);
        await _payments.SaveChangesAsync(cancellationToken);
        return ApiResponse<PaymentDto>.Ok(ToDto(payment), "Refund processed.");
    }

    private static PaymentDto ToDto(Payment payment) =>
        new(payment.Id, payment.OrderId, payment.Amount, payment.PaymentMethod, payment.PaymentStatus, payment.PaidAt);
}
