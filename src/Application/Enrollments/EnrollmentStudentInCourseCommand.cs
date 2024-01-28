namespace Application.Enrollments;
public record EnrollmentStudentInCourseCommand(Guid StudentId, Guid CourseId) : IRequest<ErrorOr<Guid>>;