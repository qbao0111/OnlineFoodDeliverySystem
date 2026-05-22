using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(AppDbContext context) : base(context)
    {
    }

    public Task<IReadOnlyList<Restaurant>> GetActiveRestaurantsAsync(CancellationToken cancellationToken = default) =>
        Context.Restaurants
            .AsNoTracking()
            .Include(restaurant => restaurant.MenuItems)
            .Where(restaurant => restaurant.Status == "Active")
            .ToListAsync(cancellationToken)
            .ContinueWith(task => (IReadOnlyList<Restaurant>)task.Result, cancellationToken);

    public Task<Restaurant?> GetWithMenuAsync(int restaurantId, CancellationToken cancellationToken = default) =>
        Context.Restaurants
            .Include(restaurant => restaurant.MenuItems)
            .FirstOrDefaultAsync(restaurant => restaurant.Id == restaurantId, cancellationToken);
}
