using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Configurations;

public class ShipmentItemConfiguration : IEntityTypeConfiguration<ShipmentItem>
{
    public void Configure(EntityTypeBuilder<ShipmentItem> builder)
    {
        builder.ToTable("ShipmentItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantity).IsRequired();

        builder.HasOne(i => i.ShipmentDocument)
               .WithMany(s => s.Items)
               .HasForeignKey(i => i.ShipmentDocumentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Resource)
               .WithMany(r => r.ShipmentItems)
               .HasForeignKey(i => i.ResourceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Measurement)
               .WithMany(m => m.ShipmentItems)
               .HasForeignKey(i => i.MeasurementId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
