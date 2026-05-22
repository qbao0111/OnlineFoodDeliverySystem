using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class MenuRepository : Repository<MenuItem>, IMenuRepository
{
    public MenuRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<MenuItem>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default) =>
        await Context.MenuItems
            .AsNoTracking()
            .Where(item => item.RestaurantId == restaurantId)
            .ToListAsync(cancellationToken);
}
