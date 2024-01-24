using Domain.Courses;
using Domain.Primitives;

namespace Application.Courses.UnitTests.CRUD;

public class CreateCourseTest
{
    [Fact]
    public async Task CreateCourse_WithValidData_ShouldReturnCourseId()
    {
        // Arrange
        var courseRepositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var courseService = new CourseService(courseRepositoryMock.Object, unitOfWorkMock.Object);

        var createCourseCommand = new CreateCourseCommand
        {
            Name = "Introduction to DDD",
            MaxStudents = 50,
            RegistrationFee = 100.00,
            StartDate = DateTime.Now.AddDays(30),
            EndDate = DateTime.Now.AddDays(90)
        };

        // Act
        var result = await courseService.Handle(createCourseCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateCourse_WithInvalidData_ShouldReturnValidationError()
    {
        // Arrange
        var courseRepositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var courseService = new CourseService(courseRepositoryMock.Object, unitOfWorkMock.Object);

        var createCourseCommand = new CreateCourseCommand
        {
            // Missing required data, causing validation error
        };

        // Act
        var result = await courseService.Handle(createCourseCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        // Add more assertions based on your validation error handling
    }
}