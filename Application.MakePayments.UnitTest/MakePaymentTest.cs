using Application.Payments.MakePayments;
using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Payments;
using Domain.Payments.Events;
using Domain.Enrollments;
using Domain.Students;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Errors = Domain.Payments.Errors;

namespace Application.Payments.UnitTest;
public class MakePaymentTest
{
    private readonly Mock<IStudentRepository> _mockstudentRepository;
    private readonly Mock<ICourseRepository> _mockcourseRepository;
    private readonly Mock<IPaymentRepository> _mockPaymentRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IPaymentGateway> _mockPaymentGateWay;
    private readonly Mock<IPublisher> _mockPublisher;
    private readonly MakePaymentCommandHandler _handler;

    public MakePaymentTest()
    {
        _mockstudentRepository = new Mock<IStudentRepository>();
        _mockcourseRepository = new Mock<ICourseRepository>();
        _mockPaymentRepository = new Mock<IPaymentRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockPaymentGateWay = new Mock<IPaymentGateway>();
        _mockPublisher = new Mock<IPublisher>();
        _handler = new MakePaymentCommandHandler(
            _mockPaymentRepository.Object,
            _mockstudentRepository.Object,
            _mockcourseRepository.Object,
            _mockUnitOfWork.Object,
            _mockPaymentGateWay.Object,
            _mockPublisher.Object);
    }

    private void SetupMocks(Guid studentId, Guid courseId)
    {

        _mockstudentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<StudentId>()))
            .ReturnsAsync((StudentId mockStudentId) =>
            {
                if (mockStudentId.Equals(new StudentId(studentId)))
                {
                    return new Student(
                 new StudentId(studentId),
                 "Juan",
                 "Pereira",
                 "pe@gmail.com",
                 PhoneNumber.Create("342-58784431"),
                 Address.Create("Argentina", "Calle falsa 123", "dto 2", "Santa Fe", "Santa Fe", "3000"));
                }

                else
                {
                    return null;
                }

            });
        _mockcourseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<CourseId>()))
            .ReturnsAsync((CourseId mockCourseId) =>
             {
                 if (mockCourseId.Equals(new CourseId(courseId)))
                 {
                     return new Course(
                         new CourseId(courseId),
                         "Basics DDD",
                         MaxStudents.Create(10),
                         new Money(100, CurrencyCode.ARS),
                         CourseDuration.Create(DateTime.Now.AddDays(30), DateTime.Now.AddDays(40)));
                 }
                 else
                 {
                     return null;
                 }

             });

        _mockPaymentGateWay.Setup(gateway => gateway.ProcessPayment(It.IsAny<decimal>(), It.IsAny<string>()))
        .ReturnsAsync((decimal amount, string cardToken) =>
        {
            if (cardToken == "valid-card-token")
            {
                return Guid.NewGuid();
            }
            else
            {
                return Errors.Payments.InvalidCardToken;
            }
        });
    }
    [Fact]
    public async Task HandlePayment_WithValidDetails_ShouldReturnSuccess()
    {
        // Arrange
        var command = new MakePaymentCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(100, CurrencyCode.ARS),
            "valid-card-token"
            );
        SetupMocks(command.StudentId, command.CourseId);

        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task HandlePayment_WithInvalidCard_ShouldReturnPaymentError()
    {
        // Arrange
        MakePaymentCommand command = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(100, CurrencyCode.ARS),
            "invalid-card-token"
        );
        SetupMocks(command.StudentId, command.CourseId);

        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Code.Should().Be(Errors.Payments.InvalidCardToken.Code);
    }
    [Fact]
    public async Task HandlePayment_WithInvalidCourseFee_ShouldReturnValidationError()
    {
        // Arrange
        MakePaymentCommand command = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(500, CurrencyCode.ARS),
            "valid-card-token"
        );
        SetupMocks(command.StudentId, command.CourseId);

        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Code.Should().Be(Errors.Payments.CourseFeeNotEquals.Code);
    }
    [Fact]
    public async Task HandlePayment_WithStundentNotFound_ShouldReturnValidationError()
    {
        // Arrange
        MakePaymentCommand command = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(100, CurrencyCode.ARS),
            "valid-card-token"
        );
        SetupMocks(Guid.NewGuid(), command.CourseId);

        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Code.Should().Be(Errors.Payments.StudentNotFound.Code);
    }
    [Fact]
    public async Task HandlePayment_WithCourseNotFound_ShouldReturnValidationError()
    {
        // Arrange
        MakePaymentCommand command = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(100, CurrencyCode.ARS),
            "valid-card-token"
        );
        SetupMocks(command.StudentId, Guid.NewGuid());

        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Code.Should().Be(Errors.Payments.CourseNotFound.Code);
    }
    [Fact]
    public async Task HandlePayment_WithZeroAmount_ShouldReturnSuccessWithoutPayment()
    {
        // Arrange
        var command = new MakePaymentCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(0, CurrencyCode.ARS),
            "valid-card-token"
        );

        SetupMocks(command.StudentId, command.CourseId);

        _mockcourseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<CourseId>()))
        .ReturnsAsync(new Course(
            new CourseId(command.CourseId),
            "Basics DDD",
            MaxStudents.Create(10),
            new Money(0, CurrencyCode.ARS),
            CourseDuration.Create(DateTime.Now.AddDays(30), DateTime.Now.AddDays(40))
            ));

        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
    }

    [Fact]
    public async Task SavePayment_ShouldPublishPaymentMadeEvent()
    {
        // Arrange
        var command = new MakePaymentCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(100, CurrencyCode.ARS),
            "valid-card-token"
            );
        SetupMocks(command.StudentId, command.CourseId);

        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        _mockPublisher.Verify(e => e.Publish(It.IsAny<PaymentMadeEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandlePayment_WithCourseFull_ShouldReturnValidationError()
    {
        // Arrange
        var command = new MakePaymentCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Money(100, CurrencyCode.ARS),
            "valid-card-token"
            );
        
        SetupMocks(command.StudentId, command.CourseId);
        
        Student studentEnrolled = new(
                 new StudentId(Guid.NewGuid()),
                 "Juan",
                 "Pereira",
                 "pe@gmail.com",
                 PhoneNumber.Create("342-58784431"),
                 Address.Create("Argentina", "Calle falsa 123", "dto 2", "Santa Fe", "Santa Fe", "3000"));
        
        _mockcourseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<CourseId>()))
           .ReturnsAsync((CourseId mockCourseId) =>
           {
               if (mockCourseId.Equals(new CourseId(command.CourseId)))
               {
                   Course course = new(
                        new CourseId(command.CourseId),
                        "Basics DDD",
                        MaxStudents.Create(1),
                        new Money(100, CurrencyCode.ARS),
                        CourseDuration.Create(DateTime.Now.AddDays(30), DateTime.Now.AddDays(40)));
                   course.AddEnrollment(new StudentEnrollment(Guid.NewGuid(), studentEnrolled, course));
                   return course;
               }
               else
               {
                   return null;
               }

           });
        // Act
        ErrorOr<Guid> result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Code.Should().Be(Errors.Payments.CourseIsFull.Code);
    }
}