using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Payments;
using Domain.Primitives;
using Domain.Students;
using Errors = Domain.Payments.Errors;

namespace Application.Payments.MakePayments;
public class MakePaymentCommandHandler : IRequestHandler<MakePaymentCommand, ErrorOr<Guid>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentGateway _paymentGateway;

    public MakePaymentCommandHandler(IPaymentRepository paymentRepository, IStudentRepository studentRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork, IPaymentGateway paymentGateway)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
        _paymentGateway = paymentGateway;
    }

    public async Task<ErrorOr<Guid>> Handle(MakePaymentCommand command, CancellationToken cancellationToken)
    {
        StudentId studentId = new(command.StudentId);
        if (await _studentRepository.GetByIdAsync(studentId) is not Student student)
        {
            return Errors.Payments.StudentNotFound;
        }
        CourseId courseId = new(command.CourseId);
        if (await _courseRepository.GetByIdAsync(courseId) is not Course course)
        {
            return Errors.Payments.CourseNotFound;
        }
        if (!course.RegistrationFee.Equals(command.Amount))
        {
            return Errors.Payments.CourseFeeNotEquals;
        }
        if (course.RegistrationFee.IsFree())
        {
            return await SavePayment(command, studentId, courseId, new PaymentId(Guid.NewGuid()), cancellationToken);
        }

        ErrorOr<Guid> paymentResult = await _paymentGateway.ProcessPayment(command.Amount.Amount, command.CardToken);

        if (paymentResult.IsError)
        {
            return paymentResult;
        }
        PaymentId paymentId = new(paymentResult.Value);

        return await SavePayment(command, studentId, courseId, paymentId, cancellationToken);
    }

    private async Task<ErrorOr<Guid>> SavePayment(MakePaymentCommand command, StudentId studentId, CourseId courseId, PaymentId paymentId, CancellationToken cancellationToken)
    {
        Payment payment = new(
            paymentId,
            studentId,
            courseId,
            command.Amount,
            command.CardToken);

        _paymentRepository.Add(payment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return paymentId.Value;
    }
}