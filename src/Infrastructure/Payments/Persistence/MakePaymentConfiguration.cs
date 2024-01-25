using Domain.Payments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Payments.Persistence;
public class MakePaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            paymentId => paymentId.Value,
            value => new PaymentId(value));

        builder.Property(p => p.StudentId)
            .IsRequired();

        builder.Property(p => p.CourseId)
            .IsRequired();

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18,2)") // Ajusta según tus necesidades
            .IsRequired();

        builder.Property(p => p.CardToken)
            .HasMaxLength(255) // Ajusta según tus necesidades
            .IsRequired();

        // Otras configuraciones específicas de la entidad MakePayment, si las tienes
    }
}
