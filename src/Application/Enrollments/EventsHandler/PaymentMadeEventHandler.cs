using Domain.Payments.Events;
using Domain.StudentEnrollments;
using Microsoft.Extensions.Logging;

namespace Application.Enrollments.EventsHandler;

public class PaymentMadeEventHandler : INotificationHandler<PaymentMadeEvent>
{
    private readonly IEnrollmentService _enrollmentService;

    public PaymentMadeEventHandler(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;

    }

    public async Task Handle(PaymentMadeEvent notification, CancellationToken cancellationToken)
    {
        var result = await _enrollmentService.EnrollStudentInCourse(notification.StudentId, notification.CourseId);
    }

}