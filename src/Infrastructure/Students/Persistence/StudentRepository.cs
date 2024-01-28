using Application.Common.Data;
using Domain.Students;
using Domain.Students.ValueObjects;

namespace Infrastructure.Students.Persistence;
public class StudentRepository : IStudentRepository
{
    private readonly IApplicationDbContext _context;

    public StudentRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Student student) => _context.Students.Add(student);

    public async Task<Student> GetByIdAsync(StudentId id) => await _context.Students.SingleOrDefaultAsync(c => c.Id == id);

    public async Task<List<Student>> GetStudentsWithCoursesInDateRange(DateTime startDate, DateTime endDate)
    {
        var studentsWithCourses = await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(se => se.Course)
            .Where(s => s.Enrollments.Any(se =>
                se.Course.CourseDuration.StartDate <= endDate && se.Course.CourseDuration.EndDate >= startDate))
            .ToListAsync();

        return studentsWithCourses;
    }
}
