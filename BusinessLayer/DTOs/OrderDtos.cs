namespace BusinessLayer.DTOs;

public record PlaceOrderDto(int CustomerId, string PaymentMethod);
public record OrderItemDto(int Id, int MenuItemId, string MenuItemName, int Quantity, decimal UnitPrice, decimal SubTotal);
public record OrderDto(int Id, int CustomerId, DateTime OrderDate, string Status, decimal TotalAmount, IReadOnlyList<OrderItemDto> Items);
public record UpdateOrderStatusDto(string Status);
