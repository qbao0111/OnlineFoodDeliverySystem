namespace BusinessLayer.DTOs;

public record AddCartItemDto(int CustomerId, int MenuItemId, int Quantity);
public record CartItemDto(int Id, int MenuItemId, string MenuItemName, int Quantity, decimal UnitPrice, decimal SubTotal);
public record CartDto(int Id, int CustomerId, IReadOnlyList<CartItemDto> Items, decimal Total);
