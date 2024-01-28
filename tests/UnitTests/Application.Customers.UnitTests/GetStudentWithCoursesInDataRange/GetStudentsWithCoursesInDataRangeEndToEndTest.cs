using Application.Courses.Create;
using Application.Payments.MakePayments;
using Application.Students.Create;
using Application.Students.GetStudentWithCoursesInDataRange;
using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Students.UnitTest.GetStudentWithCoursesInDataRange;
public class GetStudentsWithCoursesInDataRangeEndToEndTest
{
    [Fact]
    public async Task GetStudentsWithCoursesInDataRangeIntegrationTest_ShouldWork()
    {        
        var app = ProgramTest.BuildApplication();

        using (var scope = app.Services.CreateScope())
        {
            //Arrange

            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            CreateStudentCommand studentCommand = new("Gonzalo", "Fernandez", "gonz96@sc.com", "342-5787327", "Country", "Line1", "Line2", "City", "State", "ZipCode");
            ErrorOr<Guid> resultStudent1 = await mediator.Send(studentCommand);

            studentCommand = new("Pedro", "Gonzales", "pg96@sc.com", "344-4237327", "Country", "Line1", "Line2", "City", "State", "ZipCode");
            ErrorOr<Guid> resultStudent2 = await mediator.Send(studentCommand);

            CreateCourseCommand courseCommand = new(
                "Introduction to DDD",
                15,
                new Money(800, CurrencyCode.ARS),
                DateTime.Now.AddDays(30),
                DateTime.Now.AddDays(90));

            ErrorOr<Guid> resultCourse1 = await mediator.Send(courseCommand);

            courseCommand = new(
                "Introduction to TDD",
                10,
                new Money(1000, CurrencyCode.ARS),
                DateTime.Now,
                DateTime.Now.AddDays(10));

            ErrorOr<Guid> resultCourse2 = await mediator.Send(courseCommand);

            MakePaymentCommand makePaymentCommand = new(resultStudent1.Value, resultCourse1.Value, new Money(800, CurrencyCode.ARS), "valid_token");

            ErrorOr<Guid> resultPayment1 = await mediator.Send(makePaymentCommand);

            makePaymentCommand = new(resultStudent2.Value, resultCourse2.Value, new Money(1000, CurrencyCode.ARS), "valid_token");

            ErrorOr<Guid> resultPayment2 = await mediator.Send(makePaymentCommand);

            GetStudentsWithCoursesInDateRangeCommand getStudentsCommand = new(DateTime.Now, DateTime.Now.AddDays(20));

            //Act

            ErrorOr<List<Student>> students1 = await mediator.Send(getStudentsCommand);

            getStudentsCommand = new(DateTime.Now, DateTime.Now.AddDays(40));

            ErrorOr<List<Student>> students2 = await mediator.Send(getStudentsCommand);

            getStudentsCommand = new(DateTime.Now.AddDays(30), DateTime.Now.AddDays(40));

            ErrorOr<List<Student>> students3 = await mediator.Send(getStudentsCommand);

            getStudentsCommand = new(DateTime.Now.AddDays(100), DateTime.Now.AddDays(140));

            ErrorOr<List<Student>> students4 = await mediator.Send(getStudentsCommand);

            getStudentsCommand = new(DateTime.Now.AddDays(10), DateTime.Now);

            ErrorOr<List<Student>> students5 = await mediator.Send(getStudentsCommand);

            //Assert

            students1.IsError.Should().BeFalse();
            students1.Value.First().Id.Value.Should().Be(resultStudent2.Value);

            students2.IsError.Should().BeFalse();
            students2.Value.First().Id.Value.Should().Be(resultStudent1.Value);
            students2.Value.Last().Id.Value.Should().Be(resultStudent2.Value);

            students3.IsError.Should().BeFalse();
            students3.Value.First().Id.Value.Should().Be(resultStudent1.Value);

            students4.IsError.Should().BeFalse();
            students4.Value.Should().BeEmpty();

            students5.IsError.Should().BeTrue();

        }
    }
}