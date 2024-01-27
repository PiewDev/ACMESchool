using Domain.Students;

namespace Application.Students.GetStudentWithCoursesInDataRange;
public record GetStudentsWithCoursesInDateRangeCommand(DateTime StartDate, DateTime EndDate) : IRequest<ErrorOr<List<Student>>>;

