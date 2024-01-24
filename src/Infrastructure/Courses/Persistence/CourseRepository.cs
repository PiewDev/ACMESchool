using Application.Data;
using Domain.Courses;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Courses.Persistence;
public class CourseRepository : ICourseRepository
{
    private readonly IApplicationDbContext _context;

    public CourseRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Course course) => _context.Courses.Add(course);
}
