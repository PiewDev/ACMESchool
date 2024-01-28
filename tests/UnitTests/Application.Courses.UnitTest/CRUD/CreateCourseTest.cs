using Application.Courses.Create;
using Domain.Courses;
using Domain.Common;
using Domain.ValueObjects;
using ErrorOr;
using Errors = Domain.Courses.Errors;
using Domain.Common.ValueObjects;

namespace Application.Courses.UnitTests.CRUD;

public class CreateCourseTest
{
    private readonly Mock<ICourseRepository> _mockCourseRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCourseCommandHandler _handler;

    public CreateCourseTest()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateCourseCommandHandler(_mockCourseRepository.Object, _mockUnitOfWork.Object);
    }
    [Fact]
    public async Task CreateCourse_WithValidData_ShouldReturnCourseId()
    {
        // Arrange   
        CreateCourseCommand createCourseCommand = new(
            "Introduction to DDD",
            50,
            new Money(100, CurrencyCode.ARS),
            DateTime.Now.AddDays(30),
            DateTime.Now.AddDays(90));

        // Act
        var result = await _handler.Handle(createCourseCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
    }

    [Fact]
    public async Task CreateCourse_WithInvalidCourseDuration_ShouldReturnValidationError()
    {
        // Arrange       
        CreateCourseCommand createCourseCommand = new(
            "Introduction to DDD",
            50,
            new Money(100, CurrencyCode.ARS),
            DateTime.Now.AddDays(30),
            DateTime.Now.AddDays(20));

        // Act
        var result = await _handler.Handle(createCourseCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Courses.CourseDurationInvalid.Code);
        result.FirstError.Description.Should().Be(Errors.Courses.CourseDurationInvalid.Description);
    }
    [Fact]
    public async Task CreateCourse_WithInvalidMaxStudents_ShouldReturnValidationError()
    {
        // Arrange
        var courseRepositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var courseService = new CreateCourseCommandHandler(courseRepositoryMock.Object, unitOfWorkMock.Object);

        CreateCourseCommand createCourseCommand = new(
            "Introduction to DDD",
            0,
            new Money(100, CurrencyCode.ARS),
            DateTime.Now.AddDays(30),
            DateTime.Now.AddDays(20));

        // Act
        var result = await _handler.Handle(createCourseCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Courses.MaxStudentsInvalidFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Courses.MaxStudentsInvalidFormat.Description);
    }
}