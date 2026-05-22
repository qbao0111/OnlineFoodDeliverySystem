using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menu;

    public MenuController(IMenuService menu)
    {
        _menu = menu;
    }

    [HttpGet("restaurants/{restaurantId:int}/menu")]
    public async Task<IActionResult> GetMenu(int restaurantId, CancellationToken cancellationToken) =>
        Ok(await _menu.GetMenuAsync(restaurantId, cancellationToken));

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpPost("menu")]
    public async Task<IActionResult> CreateMenuItem(UpsertMenuItemDto request, CancellationToken cancellationToken) =>
        Ok(await _menu.CreateMenuItemAsync(request, cancellationToken));

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpDelete("menu/{id:int}")]
    public async Task<IActionResult> DeleteMenuItem(int id, CancellationToken cancellationToken) =>
        await _menu.DeleteMenuItemAsync(id, cancellationToken) ? NoContent() : NotFound();
}
