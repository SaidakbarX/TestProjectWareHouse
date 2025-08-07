using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<Balance> Balances { get; set; }
    public DbSet<ReceptionDocument> ReceptionDocuments { get; set; }
    public DbSet<ShipmentDocument> ShipmentDocuments { get; set; }



    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
