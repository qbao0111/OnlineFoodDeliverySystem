using BusinessLayer.Interfaces;
using BusinessLayer.PaymentStrategies;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IDeliveryService, DeliveryService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IAdminService, AdminService>();

        services.AddScoped<IPaymentStrategy, MomoPaymentStrategy>();
        services.AddScoped<IPaymentStrategy, ZaloPayPaymentStrategy>();
        services.AddScoped<IPaymentStrategy, CreditCardPaymentStrategy>();

        return services;
    }
}
