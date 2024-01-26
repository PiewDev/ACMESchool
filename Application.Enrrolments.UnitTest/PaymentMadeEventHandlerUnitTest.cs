using Application.Enrollments.EventsHandler;
using Domain.Courses.ValueObjects;
using Domain.Payments.Events;
using Domain.StudentEnrollments;
using Domain.Students;

namespace Application.Enrollments.UnitTest;
public class PaymentMadeEventHandlerUnitTest
{
    private readonly Mock<IEnrollmentService> _mockEnrollmentService;

    public PaymentMadeEventHandlerUnitTest()
    {
        _mockEnrollmentService = new Mock<IEnrollmentService>();
    }

    [Fact]
    public async Task Handle_PaymentMadeEvent_Success()
    {
        // Arrange
        PaymentMadeEvent paymentMadeEvent = new(Guid.NewGuid(), new StudentId(Guid.NewGuid()), new CourseId(Guid.NewGuid()));

        var handler = new PaymentMadeEventHandler(_mockEnrollmentService.Object);

        // Act
        await handler.Handle(paymentMadeEvent, CancellationToken.None);

        // Assert
        _mockEnrollmentService.Verify(service => service.EnrollStudentInCourse(paymentMadeEvent.StudentId, paymentMadeEvent.CourseId), Times.Once);

    }
}