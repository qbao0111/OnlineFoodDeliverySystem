namespace BusinessLayer.PaymentStrategies;

public class ZaloPayPaymentStrategy : IPaymentStrategy
{
    public string Method => "ZaloPay";
    public Task<bool> PayAsync(decimal amount, CancellationToken cancellationToken = default) => Task.FromResult(amount > 0);
}
