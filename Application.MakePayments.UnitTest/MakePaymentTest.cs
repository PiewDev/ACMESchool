using Application.Payments.MakePayments;
using Domain.Courses;
using Domain.Payments;
using Domain.Students;

namespace Application.Payments.UnitTest;
public class MakePaymentTest
{
    private readonly Mock<IStudentRepository> _mockstudentRepository;
    private readonly Mock<ICourseRepository> _mockcourseRepository;
    private readonly Mock<IPaymentRepository> _mockPaymentRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly MakePaymentCommandHandler _handler;

    public MakePaymentTest()
    {
        _mockstudentRepository = new Mock<IStudentRepository>();
        _mockcourseRepository = new Mock<ICourseRepository>();
        _mockPaymentRepository = new Mock<IPaymentRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new MakePaymentCommandHandler(_mockPaymentRepository.Object, _mockstudentRepository.Object, _mockcourseRepository.Object, _mockUnitOfWork.Object);
    }
    [Fact]
    public async Task HandlePayment_WithValidDetails_ShouldReturnSuccess()
    {
        // Arrange
        var paymentCommand = new MakePaymentCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            100.00m,
            "valid-card-token"
        );

        // Act
        var result = await _handler.Handle(paymentCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
    }

    [Fact]
    public async Task HandlePayment_WithInvalidCard_ShouldReturnPaymentError()
    {
        // Arrange
        var paymentCommand = new MakePaymentCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            100.00m,
            "invalid-card-token" 
        );

        // Act
        var result = await _handler.Handle(paymentCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Code.Should().Be("InvalidCard");
    }

    [Fact]
    public async Task HandlePayment_WithZeroAmount_ShouldReturnSuccessWithoutPayment()
    {
        // Arrange
        var paymentCommand = new MakePaymentCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            0,
            "valid-card-token"
        );

        // Act
        var result = await _handler.Handle(paymentCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
    }
}