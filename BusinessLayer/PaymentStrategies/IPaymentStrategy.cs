namespace BusinessLayer.PaymentStrategies;

public interface IPaymentStrategy
{
    string Method { get; }
    Task<bool> PayAsync(decimal amount, CancellationToken cancellationToken = default);
}
