namespace BusinessLayer.PaymentStrategies;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public string Method => "CreditCard";
    public Task<bool> PayAsync(decimal amount, CancellationToken cancellationToken = default) => Task.FromResult(amount > 0);
}
