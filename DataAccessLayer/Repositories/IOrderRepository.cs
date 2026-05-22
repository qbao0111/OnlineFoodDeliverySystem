using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetDetailsAsync(int orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default);
}
