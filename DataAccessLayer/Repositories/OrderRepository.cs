using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public Task<Order?> GetDetailsAsync(int orderId, CancellationToken cancellationToken = default) =>
        Context.Orders
            .Include(order => order.Items)
            .ThenInclude(item => item.MenuItem)
            .Include(order => order.Payment)
            .Include(order => order.Delivery)
            .FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);

    public async Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default) =>
        await Context.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .Where(order => order.CustomerId == customerId)
            .OrderByDescending(order => order.OrderDate)
            .ToListAsync(cancellationToken);
}
