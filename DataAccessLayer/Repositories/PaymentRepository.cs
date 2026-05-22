using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }

    public Task<Payment?> GetByOrderAsync(int orderId, CancellationToken cancellationToken = default) =>
        Context.Payments.FirstOrDefaultAsync(payment => payment.OrderId == orderId, cancellationToken);
}
