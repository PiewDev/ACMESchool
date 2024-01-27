using Domain.Payments;
using ErrorOr;

namespace Infrastructure.Payments.Services;
public class PaymentGateway : IPaymentGateway
{
    public async Task<ErrorOr<Guid>> ProcessPayment(decimal amount, string cardToken)
    {
        return Guid.NewGuid();
    }
}