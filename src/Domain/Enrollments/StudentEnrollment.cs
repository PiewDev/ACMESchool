using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Enrollments.ValueObjects;
using Domain.Students;
using Domain.Students.ValueObjects;

namespace Domain.Enrollments;
public class StudentEnrollment
{
    public StudentEnrollmentId Id { get; private set; }
    public StudentId StudentId { get; private set; }
    public CourseId CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public Student Student { get; private set; }
    public Course Course { get; private set; }

    public StudentEnrollment(Guid id, Student student, Course course)
    {
        Id = new StudentEnrollmentId(Guid.NewGuid());
        Student = student;
        Course = course;
        StudentId = student.Id;
        CourseId = course.Id;
        EnrollmentDate = DateTime.UtcNow;
    }
    private StudentEnrollment() { }
}