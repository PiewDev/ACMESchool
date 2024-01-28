using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Enrollments;
using Domain.Common;
using Domain.ValueObjects;
using Domain.Common.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Students;
public sealed class Student : AggregateRoot
{
    public Student(StudentId id, string name, string lastName, string email,
    PhoneNumber phoneNumber, Address address)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    private Student()
    {

    }

    public StudentId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{Name} {LastName}";
    public string Email { get; private set; } = string.Empty;
    public PhoneNumber PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }

    private readonly List<StudentEnrollment> _enrollments = new List<StudentEnrollment>();
    public IReadOnlyList<StudentEnrollment> Enrollments => _enrollments.AsReadOnly();

    public ErrorOr<StudentEnrollment> EnrollInCourse(Course course)
    {
        if (course == null)
        {
            return Errors.Student.CourseNullReference;
        }
        StudentEnrollment enrollment = new(Guid.NewGuid(), this, course);
        _enrollments.Add(enrollment);
        return enrollment;
    }
    public static Student UpdateStudent(Guid id, string name, string lastName, string email, PhoneNumber phoneNumber, Address address)
    {
        return new Student(new StudentId(id), name, lastName, email, phoneNumber, address);
    }
    public bool IsEnrolledInCourse(CourseId courseId)
    {
        return Enrollments.Any(e => e.CourseId == courseId);
    }
}
