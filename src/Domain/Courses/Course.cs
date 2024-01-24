using Domain.Courses.ValueObjects;
using Domain.Primitives;
using Domain.Students;
using Domain.ValueObjects;

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

    public Course(string name, MaxStudents maxStudents, Money registrationFee, CourseDuration courseDuration)
    {
        Id = new CourseId(Guid.NewGuid());
        Name = name;
        MaxStudents = maxStudents;
        RegistrationFee = registrationFee;
        CourseDuration = courseDuration;
    }


    public void EnrollStudent(StudentId studentId)
    {
        _enrollments.Add(new StudentEnrollment(studentId, Id));
    }


}
