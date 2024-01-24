using Domain.Courses;
using Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Student> Students { get; set; }
    DbSet<Course> Courses { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}