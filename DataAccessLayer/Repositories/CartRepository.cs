using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    public Task<Cart?> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default) =>
        Context.Carts
            .Include(cart => cart.Items)
            .ThenInclude(item => item.MenuItem)
            .FirstOrDefaultAsync(cart => cart.CustomerId == customerId, cancellationToken);
}
