using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _payments;

    public PaymentsController(IPaymentService payments)
    {
        _payments = payments;
    }

    [HttpPost]
    public async Task<IActionResult> MakePayment(PaymentRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _payments.ProcessPaymentAsync(request, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("orders/{orderId:int}/refund")]
    public async Task<IActionResult> RefundPayment(int orderId, CancellationToken cancellationToken)
    {
        var result = await _payments.RefundPaymentAsync(orderId, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
