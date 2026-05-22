using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Driver")]
[Route("api/[controller]")]
public class DeliveriesController : ControllerBase
{
    private readonly IDeliveryService _deliveries;

    public DeliveriesController(IDeliveryService deliveries)
    {
        _deliveries = deliveries;
    }

    [HttpGet("tasks")]
    public async Task<IActionResult> ViewTasks(CancellationToken cancellationToken) =>
        Ok(await _deliveries.ViewTasksAsync(cancellationToken));

    [HttpPost("accept")]
    public async Task<IActionResult> AcceptTask(AcceptDeliveryDto request, CancellationToken cancellationToken)
    {
        var result = await _deliveries.AcceptTaskAsync(request, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPatch("{deliveryId:int}/status")]
    public async Task<IActionResult> UpdateStatus(int deliveryId, UpdateDeliveryStatusDto request, CancellationToken cancellationToken)
    {
        var result = await _deliveries.UpdateStatusAsync(deliveryId, request.Status, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
