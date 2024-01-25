namespace Application.Payments.MakePayments;
public record MakePaymentCommand(
Guid StudentId,
Guid CourseId,
decimal Amount,
string CardToken) : IRequest<ErrorOr<Guid>>;