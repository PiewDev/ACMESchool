using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Primitives;
using Errors = Domain.Courses.Errors;

namespace Application.Courses.Create;
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<Guid>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        if (MaxStudents.Create(command.MaxStudents) is not MaxStudents maxStudents)
        {
            return Errors.Courses.MaxStudentsInvalidFormat;
        }
        if (CourseDuration.Create(command.StartDate, command.EndDate) is not CourseDuration maxCourseDuration)
        {
            return Errors.Courses.CourseDurationInvalid;
        }

        Course course = new(
            new CourseId(Guid.NewGuid()),
            command.Name,
            maxStudents,
            command.RegistrationFee,
            maxCourseDuration);

        _courseRepository.Add(course);

        await _unitOfWork.SaveChangesAsync();

        return course.Id.Value;
    }
}