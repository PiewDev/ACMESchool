using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Payments;
public class Payment
{
    public PaymentId Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public decimal Amount { get; private set; }
    public string CardToken { get; private set; }

    // Otras propiedades de la entidad, si las tienes

    private Payment() { }
}
