using Domain.Courses;
using Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Courses.Persistence;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            courseId => courseId.Value,
            value => new CourseId(value));

        builder.Property(c => c.Name).HasMaxLength(255);

        builder.OwnsOne(c => c.MaxStudents, maxStudentsBuilder =>
        {
            maxStudentsBuilder.Property(ms => ms.Value)
                .HasColumnName("MaxStudents")
                .IsRequired();
        });

        builder.OwnsOne(c => c.RegistrationFee, registrationFeeBuilder =>
        {
            registrationFeeBuilder.Property(rf => rf.Amount)
                .HasColumnName("RegistrationFeeAmount")
                .IsRequired();

            registrationFeeBuilder.Property(rf => rf.Currency)
                .HasColumnName("RegistrationFeeCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(c => c.CourseDuration, courseDurationBuilder =>
        {
            courseDurationBuilder.Property(cd => cd.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();

            courseDurationBuilder.Property(cd => cd.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();
        });

        builder.HasMany(c => c.Enrollments)
               .WithOne()
               .HasForeignKey(e => e.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}