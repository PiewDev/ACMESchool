using Domain.Courses.ValueObjects;
using Domain.Payments;
using Domain.Students;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Payments.Persistence;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            paymentId => paymentId.Value,
            value => new PaymentId(value));

        builder.Property(p => p.StudentId)
            .IsRequired()
            .HasConversion(courseId => courseId.Value, value => new StudentId(value));

        builder.Property(p => p.CourseId)
            .IsRequired()
            .HasConversion(courseId => courseId.Value,value => new CourseId(value));

        builder.OwnsOne(p => p.Amount, amountBuilder =>
        {
            amountBuilder.Property(m => m.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            amountBuilder.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(p => p.CardToken)
            .HasMaxLength(255)
            .IsRequired();

    }
}
