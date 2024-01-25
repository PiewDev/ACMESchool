using Domain.Courses;
using Domain.Payments;
using Domain.Primitives;
using Domain.Students;

namespace Application.Payments.MakePayments;
public class MakePaymentCommandHandler : IRequestHandler<MakePaymentCommand, ErrorOr<Guid>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository; 
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MakePaymentCommandHandler(IPaymentRepository paymentRepository, IStudentRepository studentRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;

    }

    public Task<ErrorOr<Guid>> Handle(MakePaymentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}