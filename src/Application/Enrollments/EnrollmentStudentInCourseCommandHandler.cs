
using Domain.Common;
using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Enrollments;
using Domain.Students;
using Domain.Students.ValueObjects;
using Errors = Domain.Enrollments.Errors;

namespace Application.Enrollments;
public class EnrollmentStudentInCourseCommandHandler : IRequestHandler<EnrollmentStudentInCourseCommand, ErrorOr<Guid>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentStudentInCourseCommandHandler(IStudentRepository studentRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Guid>> Handle(EnrollmentStudentInCourseCommand command, CancellationToken cancellationToken)
    {
        StudentId studentId = new(command.StudentId);
        CourseId courseId = new(command.CourseId);

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
