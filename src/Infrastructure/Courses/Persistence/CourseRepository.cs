using Application.Data;
using Domain.Courses;
using Domain.Courses.ValueObjects;

namespace Infrastructure.Courses.Persistence;
public class CourseRepository : ICourseRepository
{
    private readonly IApplicationDbContext _context;

    public CourseRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Course course) => _context.Courses.Add(course);
    public async Task<Course> GetByIdAsync(CourseId id) => await _context.Courses.SingleOrDefaultAsync(c => c.Id == id);
}
