using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<IReadOnlyList<Notification>> GetByUserAsync(int userId, CancellationToken cancellationToken = default);
}
