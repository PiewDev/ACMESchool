using Application.Enrollments.EventsHandler;
using Domain.Courses.ValueObjects;
using Domain.Payments.Events;
using Domain.Enrollments;
using Domain.Students;
using Domain.Students.ValueObjects;
using MediatR;

namespace Application.Enrollments.UnitTest;
public class PaymentMadeEventHandlerUnitTest
{
    private readonly Mock<IMediator> _mockMediator;

    public PaymentMadeEventHandlerUnitTest()
    {
        _mockMediator = new Mock<IMediator>();
    }

    [Fact]
    public async Task Handle_PaymentMadeEvent_Success()
    {
        // Arrange
        PaymentMadeEvent paymentMadeEvent = new(Guid.NewGuid(), new StudentId(Guid.NewGuid()), new CourseId(Guid.NewGuid()));

        PaymentMadeEventHandler handler = new(_mockMediator.Object);

        // Act
        await handler.Handle(paymentMadeEvent, CancellationToken.None);
      
        // Assert
        _mockMediator.Verify(m => m.Send(It.IsAny<EnrollmentStudentInCourseCommand>(), CancellationToken.None), Times.Once);

    }
}