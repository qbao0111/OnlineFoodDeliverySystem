using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class DeliveryRepository : Repository<Delivery>, IDeliveryRepository
{
    public DeliveryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Delivery>> GetAvailableTasksAsync(CancellationToken cancellationToken = default) =>
        await Context.Deliveries
            .AsNoTracking()
            .Include(delivery => delivery.Order)
            .Where(delivery => delivery.DriverId == null || delivery.DeliveryStatus == "WaitingForDriver")
            .ToListAsync(cancellationToken);
}
