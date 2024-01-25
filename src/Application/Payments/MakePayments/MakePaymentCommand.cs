using Domain.ValueObjects;

namespace Application.Payments.MakePayments;
public record MakePaymentCommand(
Guid StudentId,
Guid CourseId,
Money Amount,
string CardToken) : IRequest<ErrorOr<Guid>>;