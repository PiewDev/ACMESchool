using Domain.Courses;
using Domain.Payments;
using Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Data;

public interface IApplicationDbContext
{
    DbSet<Student> Students { get; set; }
    DbSet<Course> Courses { get; set; }
    DbSet<Payment> Payments { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}