using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurants;

    public RestaurantService(IRestaurantRepository restaurants)
    {
        _restaurants = restaurants;
    }

    public async Task<IReadOnlyList<RestaurantDto>> BrowseRestaurantsAsync(CancellationToken cancellationToken = default) =>
        (await _restaurants.GetActiveRestaurantsAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<RestaurantDto?> GetRestaurantAsync(int id, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurants.GetWithMenuAsync(id, cancellationToken);
        return restaurant is null ? null : ToDto(restaurant);
    }

    public async Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto request, CancellationToken cancellationToken = default)
    {
        var restaurant = new Restaurant
        {
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            RestaurantOwnerId = request.RestaurantOwnerId,
            Status = "Active"
        };
        await _restaurants.AddAsync(restaurant, cancellationToken);
        await _restaurants.SaveChangesAsync(cancellationToken);
        return ToDto(restaurant);
    }

    private static RestaurantDto ToDto(Restaurant restaurant) =>
        new(restaurant.Id, restaurant.Name, restaurant.Address, restaurant.PhoneNumber, restaurant.Status);
}
