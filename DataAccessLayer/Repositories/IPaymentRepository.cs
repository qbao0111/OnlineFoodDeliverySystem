using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByOrderAsync(int orderId, CancellationToken cancellationToken = default);
}
