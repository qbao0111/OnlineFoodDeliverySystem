using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Review>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default) =>
        await Context.Reviews
            .AsNoTracking()
            .Where(review => review.RestaurantId == restaurantId)
            .OrderByDescending(review => review.CreatedAt)
            .ToListAsync(cancellationToken);
}
