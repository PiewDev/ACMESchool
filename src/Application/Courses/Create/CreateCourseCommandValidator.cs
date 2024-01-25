namespace Application.Courses.Create;
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(c => c.MaxStudents)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.RegistrationFee)
            .NotNull();

        RuleFor(c => c.StartDate)
            .NotEmpty()
            .Must((command, startDate) => startDate < command.EndDate)
            .WithMessage("La fecha de inicio debe ser anterior a la fecha de finalización.");

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .Must((command, endDate) => endDate > command.StartDate)
            .WithMessage("La fecha de finalización debe ser posterior a la fecha de inicio.");
    }
}
