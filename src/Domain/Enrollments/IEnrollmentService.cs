using Domain.Courses.ValueObjects;
using Domain.Students;

namespace Domain.Enrollments;
public interface IEnrollmentService
{
    Task<ErrorOr<Guid>> EnrollStudentInCourse(StudentId studentId, CourseId courseId);

}