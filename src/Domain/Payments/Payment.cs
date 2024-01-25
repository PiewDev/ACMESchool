using Domain.Courses.ValueObjects;
using Domain.Students;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Payments;
public class Payment
{
    public PaymentId Id { get; private set; }
    public StudentId StudentId { get; private set; }
    public CourseId CourseId { get; private set; }
    public Money Amount { get; private set; }
    public string CardToken { get; private set; }

    // Otras propiedades de la entidad, si las tienes

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
