using Domain.Students;

namespace Application.Students.GetStudentWithCoursesInDataRange;
public class GetStudentsWithCoursesInDateRangeCommandHandler : IRequestHandler<GetStudentsWithCoursesInDateRangeCommand, ErrorOr<List<Student>>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsWithCoursesInDateRangeCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ErrorOr<List<Student>>> Handle(GetStudentsWithCoursesInDateRangeCommand command, CancellationToken cancellationToken)
    {
        if (command.StartDate > command.EndDate)
        {
            return Errors.Student.InvalidDateRangeError;
        }

        List<Student> students = await _studentRepository.GetStudentsWithCoursesInDateRange(command.StartDate, command.EndDate);

        return students;
    }
}
