using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Common;
using Domain.Students;
using Domain.ValueObjects;
using Domain.Common.ValueObjects;
using Domain.Students.ValueObjects;
using ErrorOr;

namespace Application.Enrollments.UnitTest;
public class EnrollStudentInCourse
{
    private Mock<IStudentRepository> _mockStudentRepository;
    private Mock<ICourseRepository> _mockCourseRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;

    public EnrollStudentInCourse()
    {
        _mockStudentRepository = new Mock<IStudentRepository>();
        _mockCourseRepository = new Mock<ICourseRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }
    private void SetupMocks(Guid studentId, Guid courseId)
    {
        _mockStudentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<StudentId>()))
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

        _mockCourseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<CourseId>()))
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

    }

    [Fact]
    public async Task EnrollStudentInCourse_Success()
    {
        // Arrange
        StudentId studentId = new(Guid.NewGuid());
        CourseId courseId = new(Guid.NewGuid());

        SetupMocks(studentId.Value, courseId.Value);

        EnrollmentStudentInCourseCommandHandler enrollmentHandler = new (
            _mockStudentRepository.Object,
            _mockCourseRepository.Object,
            _mockUnitOfWork.Object);

        EnrollmentStudentInCourseCommand command = new(studentId.Value, courseId.Value);
        // Act
        ErrorOr<Guid> result = await enrollmentHandler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
    }

    [Fact]
    public async Task EnrollStudentInCourse_Failure()
    {
        // Arrange
        StudentId studentId = new(Guid.NewGuid());
        CourseId courseId = new(Guid.NewGuid());

        _mockStudentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<StudentId>()))
            .ReturnsAsync((StudentId mockStudentId) => null);
        
        EnrollmentStudentInCourseCommandHandler enrollmentHandler = new(
            _mockStudentRepository.Object,
            _mockCourseRepository.Object,
            _mockUnitOfWork.Object);

        EnrollmentStudentInCourseCommand command = new(studentId.Value, courseId.Value);

        // Act
        ErrorOr<Guid> result = await enrollmentHandler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
    }
}