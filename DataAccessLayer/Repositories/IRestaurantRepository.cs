using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface IRestaurantRepository : IRepository<Restaurant>
{
    Task<IReadOnlyList<Restaurant>> GetActiveRestaurantsAsync(CancellationToken cancellationToken = default);
    Task<Restaurant?> GetWithMenuAsync(int restaurantId, CancellationToken cancellationToken = default);
}
