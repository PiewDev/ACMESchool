using Application.Students.Create;
using Domain.Primitives;

namespace Application.Students.UnitTests.CRUD;
public class CreateStudentTest
{
    private readonly Mock<IStudentRepository> _mockStudentRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateStudentCommandHandler _handler;

    public CreateStudentTest()
    {
        _mockStudentRepository = new Mock<IStudentRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateStudentCommandHandler(_mockStudentRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleCreateStudent_WhenAllDataIsValid_ShouldReturnStudentId()
    {
        // Arrange

        CreateStudentCommand command = new("Gonzalo", "Fernandez", "gonz96@sc.com", "640434323", "Country", "Line1", "Line2", "City", "State", "ZipCode");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeEmpty();
    }
}