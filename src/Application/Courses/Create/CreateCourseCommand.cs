using Domain.ValueObjects;

namespace Application.Courses.Create;
public record CreateCourseCommand(
    string Name,
    int MaxStudents,
    Money RegistrationFee,
    DateTime StartDate,
    DateTime EndDate) : IRequest<ErrorOr<Guid>>;
