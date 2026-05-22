namespace BusinessLayer.PaymentStrategies;

public class MomoPaymentStrategy : IPaymentStrategy
{
    public string Method => "Momo";
    public Task<bool> PayAsync(decimal amount, CancellationToken cancellationToken = default) => Task.FromResult(amount > 0);
}
