using Application.Data;
using Domain.Payments;

namespace Infrastructure.Payments.Persistence;
public class PaymentRepository : IPaymentRepository
{
    private readonly IApplicationDbContext _context;

    public PaymentRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Payment course) => _context.Payments.Add(course);
}
