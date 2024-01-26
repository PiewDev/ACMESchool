using Domain.Courses.ValueObjects;
using Domain.StudentEnrollments;
using Domain.Students;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.StudentEnrollments;
public class StudentEnrollmentConfiguration : IEntityTypeConfiguration<StudentEnrollment>
{
    public void Configure(EntityTypeBuilder<StudentEnrollment> builder)
    {
        builder.ToTable("StudentEnrollments");

        builder.HasKey(se => se.Id);

        builder.Property(se => se.Id)
               .HasConversion(id => id.Value,
                              value => new StudentEnrollmentId(value));

        builder.HasOne(se => se.Student)
               .WithMany(s => s.Enrollments)
               .HasForeignKey(se => se.StudentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(se => se.Course)
               .WithMany(c => c.Enrollments)
               .HasForeignKey(se => se.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
