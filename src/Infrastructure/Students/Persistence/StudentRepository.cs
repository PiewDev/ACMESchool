using Application.Data;
using Domain.Students;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Students.Persistence;
public class StudentRepository : IStudentRepository
{
    private readonly IApplicationDbContext _context;

    public StudentRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Student student) => _context.Students.Add(student);
}
