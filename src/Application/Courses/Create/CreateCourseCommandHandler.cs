using Domain.Courses;
using Domain.Primitives;
using Domain.Students;

namespace Application.Courses.Create;
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<Guid>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public Task<ErrorOr<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}