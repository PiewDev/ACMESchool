using Domain.Students;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Students.Persistence;
public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Student student) => _context.Students.Add(student);
}
