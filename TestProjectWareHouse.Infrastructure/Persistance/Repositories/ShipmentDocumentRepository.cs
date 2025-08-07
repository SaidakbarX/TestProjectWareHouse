using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Repositories;

public class ShipmentDocumentRepository : IShipmentDocumentRepository
{
    private readonly AppDbContext _context;

    public ShipmentDocumentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ShipmentDocument>> GetAllAsync() =>
        await _context.ShipmentDocuments.Include(s => s.Items).ThenInclude(x=>x.Measurement)
        .Include(x=>x.Items).ThenInclude(x=>x.Resource).Include(x=>x.Client).ToListAsync();

    public async Task<ShipmentDocument?> GetByIdAsync(long id) =>
        await _context.ShipmentDocuments.FindAsync(id);

    public async Task<bool> ExistsByNumberAsync(string number) =>
        await _context.ShipmentDocuments.AnyAsync(d => d.Number == number);

    public async Task<ShipmentDocument?> GetWithItemsAsync(long id) =>
        await _context.ShipmentDocuments.Include(d => d.Items)
                                        .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(ShipmentDocument doc) =>
        await _context.ShipmentDocuments.AddAsync(doc);

    public void Update(ShipmentDocument doc) => _context.ShipmentDocuments.Update(doc);

    public void Delete(ShipmentDocument doc) => _context.ShipmentDocuments.Remove(doc);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}