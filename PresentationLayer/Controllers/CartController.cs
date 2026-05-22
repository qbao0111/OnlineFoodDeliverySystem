using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cart;

    public CartController(ICartService cart)
    {
        _cart = cart;
    }

    [HttpGet("{customerId:int}")]
    public async Task<IActionResult> ViewCart(int customerId, CancellationToken cancellationToken) =>
        Ok(await _cart.GetCartAsync(customerId, cancellationToken));

    [HttpPost("items")]
    public async Task<IActionResult> AddToCart(AddCartItemDto request, CancellationToken cancellationToken)
    {
        var result = await _cart.AddItemAsync(request, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{customerId:int}/items/{cartItemId:int}")]
    public async Task<IActionResult> RemoveItem(int customerId, int cartItemId, CancellationToken cancellationToken)
    {
        var result = await _cart.RemoveItemAsync(customerId, cartItemId, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
