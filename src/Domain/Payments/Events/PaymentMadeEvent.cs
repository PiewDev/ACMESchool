using Domain.Courses.ValueObjects;
using Domain.Common;
using Domain.Students.ValueObjects;

namespace Domain.Payments.Events;
public record PaymentMadeEvent : DomainEvent
{
    public StudentId StudentId { get; }
    public CourseId CourseId { get; }

    public PaymentMadeEvent(Guid id, StudentId studentId, CourseId courseId) : base(id)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
}
