using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Configurations;

public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.ToTable("Measurements");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(m => m.Name).IsUnique();

        builder.Property(m => m.IsArchived).IsRequired();
    }
}

