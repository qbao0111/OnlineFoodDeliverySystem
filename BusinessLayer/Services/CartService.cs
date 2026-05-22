using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _carts;
    private readonly IMenuRepository _menu;

    public CartService(ICartRepository carts, IMenuRepository menu)
    {
        _carts = carts;
        _menu = menu;
    }

    public async Task<ApiResponse<CartDto>> GetCartAsync(int customerId, CancellationToken cancellationToken = default)
    {
        var cart = await EnsureCartAsync(customerId, cancellationToken);
        return ApiResponse<CartDto>.Ok(ToDto(cart));
    }

    public async Task<ApiResponse<CartDto>> AddItemAsync(AddCartItemDto request, CancellationToken cancellationToken = default)
    {
        if (request.Quantity <= 0) return ApiResponse<CartDto>.Fail("Quantity must be greater than zero.");

        var menuItem = await _menu.GetByIdAsync(request.MenuItemId, cancellationToken);
        if (menuItem is null || !menuItem.IsAvailable) return ApiResponse<CartDto>.Fail("Menu item is not available.");

        var cart = await EnsureCartAsync(request.CustomerId, cancellationToken);
        var existingItem = cart.Items.FirstOrDefault(item => item.MenuItemId == request.MenuItemId);
        if (existingItem is null)
        {
            cart.Items.Add(new CartItem { MenuItemId = menuItem.Id, Quantity = request.Quantity, UnitPrice = menuItem.Price });
        }
        else
        {
            existingItem.Quantity += request.Quantity;
            existingItem.UnitPrice = menuItem.Price;
        }

        await _carts.SaveChangesAsync(cancellationToken);
        cart = await EnsureCartAsync(request.CustomerId, cancellationToken);
        return ApiResponse<CartDto>.Ok(ToDto(cart), "Cart updated.");
    }

    public async Task<ApiResponse<CartDto>> RemoveItemAsync(int customerId, int cartItemId, CancellationToken cancellationToken = default)
    {
        var cart = await EnsureCartAsync(customerId, cancellationToken);
        var item = cart.Items.FirstOrDefault(cartItem => cartItem.Id == cartItemId);
        if (item is null) return ApiResponse<CartDto>.Fail("Cart item was not found.");

        cart.Items.Remove(item);
        await _carts.SaveChangesAsync(cancellationToken);
        return ApiResponse<CartDto>.Ok(ToDto(cart), "Cart item removed.");
    }

    private async Task<Cart> EnsureCartAsync(int customerId, CancellationToken cancellationToken)
    {
        var cart = await _carts.GetByCustomerAsync(customerId, cancellationToken);
        if (cart is not null) return cart;

        cart = new Cart { CustomerId = customerId };
        await _carts.AddAsync(cart, cancellationToken);
        await _carts.SaveChangesAsync(cancellationToken);
        return cart;
    }

    private static CartDto ToDto(Cart cart)
    {
        var items = cart.Items.Select(item => new CartItemDto(
            item.Id,
            item.MenuItemId,
            item.MenuItem?.Name ?? string.Empty,
            item.Quantity,
            item.UnitPrice,
            item.Quantity * item.UnitPrice)).ToList();

        return new CartDto(cart.Id, cart.CustomerId, items, items.Sum(item => item.SubTotal));
    }
}
