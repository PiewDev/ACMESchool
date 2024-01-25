namespace Domain.Payments;
public interface IPaymentGateway
{
    Task<ErrorOr<Guid>> ProcessPayment(decimal amount, string cardToken);
}
