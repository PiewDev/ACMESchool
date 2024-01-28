using Domain.Courses.ValueObjects;
using Domain.Common;
using Domain.Enrollments;
using Domain.Common.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Courses;
public class Course : AggregateRoot
{
    public CourseId Id { get; private set; }
    public string Name { get; private set; }
    public MaxStudents MaxStudents { get; private set; }
    public Money RegistrationFee { get; private set; }
    public CourseDuration CourseDuration { get; private set; }

    private readonly List<StudentEnrollment> _enrollments = new List<StudentEnrollment>();
    public IReadOnlyList<StudentEnrollment> Enrollments => _enrollments.AsReadOnly();

    private Course()
    {
            
    }
    public Course(CourseId id, string name, MaxStudents maxStudents, Money registrationFee, CourseDuration courseDuration)
    {
        Id = id;
        Name = name;
        MaxStudents = maxStudents;
        RegistrationFee = registrationFee;
        CourseDuration = courseDuration;
    }
    public bool IsFull()
    {
        return Enrollments.Count >= MaxStudents.Value;
    }
    public bool HasStudentEnrolled(StudentId studentId)
    {
        return Enrollments.Any(e => e.StudentId == studentId);
    }
    public ErrorOr<StudentEnrollment> AddEnrollment(StudentEnrollment enrollment)
    {
        _enrollments.Add(enrollment);
        return enrollment;
    }
}
