using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orders;

    public OrdersController(IOrderService orders)
    {
        _orders = orders;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderDto request, CancellationToken cancellationToken)
    {
        var result = await _orders.PlaceOrderAsync(request, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{orderId:int}/track")]
    public async Task<IActionResult> TrackOrder(int orderId, CancellationToken cancellationToken)
    {
        var result = await _orders.TrackOrderAsync(orderId, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost("{orderId:int}/cancel")]
    public async Task<IActionResult> CancelOrder(int orderId, CancellationToken cancellationToken)
    {
        var result = await _orders.CancelOrderAsync(orderId, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpPatch("{orderId:int}/status")]
    public async Task<IActionResult> UpdateStatus(int orderId, UpdateOrderStatusDto request, CancellationToken cancellationToken)
    {
        var result = await _orders.UpdateOrderStatusAsync(orderId, request.Status, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
