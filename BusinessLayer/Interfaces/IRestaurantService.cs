using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface IRestaurantService
{
    Task<IReadOnlyList<RestaurantDto>> BrowseRestaurantsAsync(CancellationToken cancellationToken = default);
    Task<RestaurantDto?> GetRestaurantAsync(int id, CancellationToken cancellationToken = default);
    Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto request, CancellationToken cancellationToken = default);
}
