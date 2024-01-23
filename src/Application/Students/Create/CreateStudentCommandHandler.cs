using Domain.Primitives;
using Domain.Students;

namespace Application.Students.Create;
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, ErrorOr<Guid>>
{
    private readonly IStudentRepository _StudentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _StudentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public Task<ErrorOr<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
