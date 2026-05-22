using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default);
}
