using Application.Data;
using Domain.Courses;
using Domain.Payments;
using Domain.Primitives;
using Domain.StudentEnrollments;
using Domain.Students;

namespace Infrastructure.Common.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students {get; set; }
    public DbSet<StudentEnrollment> StudentEnrollments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ACMEMemoryDB");
    }
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e => e.GetDomainEvents());

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}