namespace BusinessLayer.DTOs;

public record PaymentRequestDto(int OrderId, decimal Amount, string PaymentMethod);
public record PaymentDto(int Id, int OrderId, decimal Amount, string PaymentMethod, string PaymentStatus, DateTime? PaidAt);
