using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Configurations;

public class ReceptionDocumentConfiguration : IEntityTypeConfiguration<ReceptionDocument>
{
    public void Configure(EntityTypeBuilder<ReceptionDocument> builder)
    {
        builder.ToTable("ReceptionDocuments");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Number)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(r => r.Number).IsUnique();

        builder.Property(r => r.Date).IsRequired();
    }
}
