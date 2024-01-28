using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Students;
using Domain.Students.ValueObjects;
using Domain.ValueObjects;

namespace Application.Students.Create;
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, ErrorOr<Guid>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
    {
        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            return Errors.Student.PhoneNumberWithBadFormat;
        }
        if (Address.Create(command.Country, command.Line1, command.Line2, command.City, command.State, command.ZipCode) is not Address adress)
        {
            return Errors.Student.AddressWithBadFormat;
        }

        Student student = new(
            new StudentId(Guid.NewGuid()),
            command.Name,
            command.LastName,
            command.Email,
            phoneNumber,
            adress);

        _studentRepository.Add(student);

        await _unitOfWork.SaveChangesAsync();

        return student.Id.Value;        
    }
}
