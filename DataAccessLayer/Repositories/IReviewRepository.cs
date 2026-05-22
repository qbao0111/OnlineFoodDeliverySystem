using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<IReadOnlyList<Review>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
}
