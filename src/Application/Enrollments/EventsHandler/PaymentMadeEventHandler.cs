using Domain.Enrollments;
using Domain.Payments.Events;

namespace Application.Enrollments.EventsHandler;

public class PaymentMadeEventHandler : INotificationHandler<PaymentMadeEvent>
{
    private readonly IMediator _mediator;

    public PaymentMadeEventHandler(IMediator mediator)
    {
        _mediator = mediator;

    }

    public async Task Handle(PaymentMadeEvent notification, CancellationToken cancellationToken)
    {
        ErrorOr<Guid> result = await _mediator.Send(new EnrollmentStudentInCourseCommand(notification.StudentId.Value, notification.CourseId.Value));
    }

}