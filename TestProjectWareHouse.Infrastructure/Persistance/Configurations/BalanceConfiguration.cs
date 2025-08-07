using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Configurations;

public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.ToTable("Balances");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Quantity).IsRequired();

        builder.HasOne(b => b.Resource)
               .WithMany(r => r.Balances)
               .HasForeignKey(b => b.ResourceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Measurement)
               .WithMany(m => m.Balances)
               .HasForeignKey(b => b.MeasurementId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.ResourceId, b.MeasurementId }).IsUnique();
    }
}
