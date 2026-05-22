using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _users;
    private readonly IRestaurantRepository _restaurants;
    private readonly IOrderRepository _orders;

    public AdminService(IUserRepository users, IRestaurantRepository restaurants, IOrderRepository orders)
    {
        _users = users;
        _restaurants = restaurants;
        _orders = orders;
    }

    public async Task<IReadOnlyList<UserDto>> ManageUsersAsync(CancellationToken cancellationToken = default) =>
        (await _users.GetAllAsync(cancellationToken)).Select(user => new UserDto(user.Id, user.FullName, user.Email, user.PhoneNumber, user.Role)).ToList();

    public async Task<ReportDto> ViewReportsAsync(CancellationToken cancellationToken = default)
    {
        var users = await _users.GetAllAsync(cancellationToken);
        var restaurants = await _restaurants.GetAllAsync(cancellationToken);
        var orders = await _orders.GetAllAsync(cancellationToken);
        return new ReportDto(users.Count, restaurants.Count, orders.Count, orders.Sum(order => order.TotalAmount));
    }
}
