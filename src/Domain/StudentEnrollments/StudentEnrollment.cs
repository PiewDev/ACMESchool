using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Students;

namespace Domain.StudentEnrollments;
public class StudentEnrollment
{
    public StudentEnrollmentId Id { get; private set; }
    public StudentId StudentId { get; private set; }
    public CourseId CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public Student Student { get; private set; }
    public Course Course { get; private set; }

    public StudentEnrollment(StudentId studentId, CourseId courseId)
    {
        Id = new StudentEnrollmentId(Guid.NewGuid());
        StudentId = studentId;
        CourseId = courseId;
        EnrollmentDate = DateTime.UtcNow;
    }
    private StudentEnrollment() { }
}