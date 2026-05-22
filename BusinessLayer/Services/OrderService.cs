using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using BusinessLayer.Validators;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class OrderService : IOrderService
{
    private readonly ICartRepository _carts;
    private readonly IOrderRepository _orders;
    private readonly IPaymentService _payments;
    private readonly INotificationService _notifications;

    public OrderService(ICartRepository carts, IOrderRepository orders, IPaymentService payments, INotificationService notifications)
    {
        _carts = carts;
        _orders = orders;
        _payments = payments;
        _notifications = notifications;
    }

    public async Task<ApiResponse<OrderDto>> PlaceOrderAsync(PlaceOrderDto request, CancellationToken cancellationToken = default)
    {
        var validation = RequestValidators.Validate(request);
        if (validation is not null) return ApiResponse<OrderDto>.Fail(validation);

        var cart = await _carts.GetByCustomerAsync(request.CustomerId, cancellationToken);
        if (cart is null || cart.Items.Count == 0) return ApiResponse<OrderDto>.Fail("Cart is empty.");

        var order = new Order
        {
            CustomerId = request.CustomerId,
            TotalAmount = cart.Items.Sum(item => item.Quantity * item.UnitPrice),
            Status = "PendingPayment",
            Items = cart.Items.Select(item => new OrderItem { MenuItemId = item.MenuItemId, Quantity = item.Quantity, UnitPrice = item.UnitPrice }).ToList(),
            Delivery = new Delivery { DeliveryStatus = "WaitingForPayment" }
        };

        await _orders.AddAsync(order, cancellationToken);
        cart.Items.Clear();
        await _orders.SaveChangesAsync(cancellationToken);

        var payment = await _payments.ProcessPaymentAsync(new PaymentRequestDto(order.Id, order.TotalAmount, request.PaymentMethod), cancellationToken);
        order.Status = payment.Success ? "Paid" : "PaymentFailed";
        order.Delivery!.DeliveryStatus = payment.Success ? "WaitingForDriver" : "PaymentFailed";
        _orders.Update(order);
        await _orders.SaveChangesAsync(cancellationToken);

        await _notifications.SendNotificationAsync(new SendNotificationDto(request.CustomerId, payment.Success ? "Payment successful." : "Payment failed.", "Order"), cancellationToken);
        var saved = await _orders.GetDetailsAsync(order.Id, cancellationToken);
        return payment.Success ? ApiResponse<OrderDto>.Ok(ToDto(saved!), "Order placed successfully.") : ApiResponse<OrderDto>.Fail("Order created but payment failed.");
    }

    public async Task<ApiResponse<OrderDto>> CancelOrderAsync(int orderId, CancellationToken cancellationToken = default) =>
        await UpdateOrderStatusAsync(orderId, "Cancelled", cancellationToken);

    public async Task<ApiResponse<OrderDto>> TrackOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var order = await _orders.GetDetailsAsync(orderId, cancellationToken);
        return order is null ? ApiResponse<OrderDto>.Fail("Order was not found.") : ApiResponse<OrderDto>.Ok(ToDto(order));
    }

    public async Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(int orderId, string status, CancellationToken cancellationToken = default)
    {
        var order = await _orders.GetDetailsAsync(orderId, cancellationToken);
        if (order is null) return ApiResponse<OrderDto>.Fail("Order was not found.");

        order.Status = status;
        _orders.Update(order);
        await _orders.SaveChangesAsync(cancellationToken);
        await _notifications.SendNotificationAsync(new SendNotificationDto(order.CustomerId, $"Order status updated to {status}.", "Order"), cancellationToken);
        return ApiResponse<OrderDto>.Ok(ToDto(order), "Order status updated.");
    }

    private static OrderDto ToDto(Order order) =>
        new(order.Id, order.CustomerId, order.OrderDate, order.Status, order.TotalAmount,
            order.Items.Select(item => new OrderItemDto(item.Id, item.MenuItemId, item.MenuItem?.Name ?? string.Empty, item.Quantity, item.UnitPrice, item.Quantity * item.UnitPrice)).ToList());
}
