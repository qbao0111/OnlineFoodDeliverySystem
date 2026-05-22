using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface IDeliveryRepository : IRepository<Delivery>
{
    Task<IReadOnlyList<Delivery>> GetAvailableTasksAsync(CancellationToken cancellationToken = default);
}
