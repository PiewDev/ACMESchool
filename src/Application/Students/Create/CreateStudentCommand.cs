namespace Application.Students.Create;
public record CreateStudentCommand(
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode) : IRequest<ErrorOr<Guid>>;
