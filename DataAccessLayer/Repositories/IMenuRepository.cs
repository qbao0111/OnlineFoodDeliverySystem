using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface IMenuRepository : IRepository<MenuItem>
{
    Task<IReadOnlyList<MenuItem>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
}
