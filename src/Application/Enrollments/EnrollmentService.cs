using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Enrollments;
using Domain.Primitives;
using Domain.Students;
using Errors = Domain.Enrollments.Errors;

namespace Application.Enrollments;
public class EnrollmentService : IEnrollmentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;


    public EnrollmentService(IStudentRepository studentRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> EnrollStudentInCourse(StudentId studentId, CourseId courseId)
    {
        if (await _studentRepository.GetByIdAsync(studentId) is not Student updatedStudent)
        {
            return Errors.StudentEnrollment.StudentNotFound;
        }
        if (await _courseRepository.GetByIdAsync(courseId) is not Course updatedCourse)
        {
            return Errors.StudentEnrollment.CourseNotFound;
        }

        ErrorOr<StudentEnrollment> enrollment = updatedStudent.EnrollInCourse(updatedCourse);

        if (enrollment.IsError)
        {
            return enrollment.Errors;
        }

        enrollment = updatedCourse.AddEnrollment(enrollment.Value);

        if (enrollment.IsError)
        {
            return enrollment.Errors;
        }

        await _unitOfWork.SaveChangesAsync();

        return enrollment.Value.Id.Value;
    }
}
