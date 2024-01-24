using Application.Courses.Create;
using Application.Students.Create;
using Domain.Courses;
using Domain.Primitives;
using Domain.Students;
using Domain.ValueObjects;
using Moq;
using System.Xml.Linq;

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

        var createCourseCommand = new CreateCourseCommand(
            "Introduction to DDD",
            50,
            new Money(100, CurrencyCode.ARS),
            DateTime.Now.AddDays(30),
            DateTime.Now.AddDays(90));

        // Act
        var result = await _handler.Handle(createCourseCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateCourse_WithInvalidData_ShouldReturnValidationError()
    {
        // Arrange
        var courseRepositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var courseService = new CreateCourseCommandHandler(courseRepositoryMock.Object, unitOfWorkMock.Object);

        CreateCourseCommand createCourseCommand = new(
            "Introduction to DDD",
            50,
            new Money(100, CurrencyCode.ARS),
            DateTime.Now.AddDays(30),
            DateTime.Now.AddDays(90));

        // Act
        var result = await courseService.Handle(createCourseCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        // Add more assertions based on your validation error handling
    }
}