using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Configurations;

public class ReceptionItemConfiguration : IEntityTypeConfiguration<ReceptionItem>
{
    public void Configure(EntityTypeBuilder<ReceptionItem> builder)
    {
        builder.ToTable("ReceptionItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantity).IsRequired();

        builder.HasOne(i => i.ReceptionDocument)
               .WithMany(d => d.Items)
               .HasForeignKey(i => i.ReceptionDocumentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Resource)
               .WithMany(r => r.ReceptionItems)
               .HasForeignKey(i => i.ResourceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Measurement)
               .WithMany(m => m.ReceptionItems)
               .HasForeignKey(i => i.MeasurementId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
