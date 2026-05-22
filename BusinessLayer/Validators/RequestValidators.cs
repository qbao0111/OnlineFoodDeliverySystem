using BusinessLayer.DTOs;

namespace BusinessLayer.Validators;

public static class RequestValidators
{
    public static string? Validate(RegisterRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains('@')) return "A valid email is required.";
        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 4) return "Password must contain at least 4 characters.";
        if (string.IsNullOrWhiteSpace(request.Role)) return "Role is required.";
        return null;
    }

    public static string? Validate(PlaceOrderDto request) =>
        request.CustomerId <= 0 ? "CustomerId is required." :
        string.IsNullOrWhiteSpace(request.PaymentMethod) ? "PaymentMethod is required." : null;

    public static string? Validate(PaymentRequestDto request) =>
        request.Amount <= 0 ? "Amount must be greater than zero." :
        string.IsNullOrWhiteSpace(request.PaymentMethod) ? "PaymentMethod is required." : null;
}
