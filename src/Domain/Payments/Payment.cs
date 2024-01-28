using Domain.Common.ValueObjects;
using Domain.Courses.ValueObjects;
using Domain.Payments.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Payments;
public class Payment
{
    public PaymentId Id { get; private set; }
    public StudentId StudentId { get; private set; }
    public CourseId CourseId { get; private set; }
    public Money Amount { get; private set; }
    public string CardToken { get; private set; }

    private Payment() { }

    public Payment(PaymentId paymentId, StudentId studentId, CourseId courseId, Money Ammount, string cardToken)
    {
        Id = paymentId;
        StudentId = studentId;
        CourseId = courseId;
        Amount = Ammount;
        CardToken = cardToken;

    }
}
