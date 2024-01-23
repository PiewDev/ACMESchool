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
        CreateStudentCommand command = new("Gonzalo", "Fernandez", "gonz96@sc.com", "342-5787327", "Country", "Line1", "Line2", "City", "State", "ZipCode");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task HandleCreateStudent_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {

        //Arrange
        CreateStudentCommand command = new("Gonzalo", "Fernandez", "gonz96@sc.com", "1213", "Country", "Line1", "Line2", "City", "State", "ZipCode");

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Student.PhoneNumberWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Student.PhoneNumberWithBadFormat.Description);
    }
    [Fact]
    public async Task HandleCreateStudent_WhenAddressFieldsAreNullOrEmpty_ShouldReturnValidationError()
    {
        // Arrange
        CreateStudentCommand command = new("Gonzalo", "Fernandez", "gonz96@sc.com", "123-4567890", "", "Line1", "Line2", "City", "State", "ZipCode");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Student.AddressWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Student.AddressWithBadFormat.Description);
    }
}
