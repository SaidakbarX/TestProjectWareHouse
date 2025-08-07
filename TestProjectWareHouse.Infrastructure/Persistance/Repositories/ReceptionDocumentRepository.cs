using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Repositories;

public class ReceptionDocumentRepository : IReceptionDocumentRepository
{
    private readonly AppDbContext _context;

    public ReceptionDocumentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReceptionDocument>> GetAllAsync() =>
        await _context.ReceptionDocuments
    .Include(r => r.Items)
        .ThenInclude(i => i.Measurement)
    .Include(r => r.Items)
        .ThenInclude(i => i.Resource)
    .ToListAsync();

    public async Task<ReceptionDocument?> GetByIdAsync(long id) =>
        await _context.ReceptionDocuments.FindAsync(id);

    public async Task<bool> ExistsByNumberAsync(string number) =>
        await _context.ReceptionDocuments.AnyAsync(d => d.Number == number);

    public async Task<ReceptionDocument?> GetWithItemsAsync(long id) =>
        await _context.ReceptionDocuments.Include(d => d.Items)
                                         .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(ReceptionDocument doc) =>
        await _context.ReceptionDocuments.AddAsync(doc);

    public void Update(ReceptionDocument doc) => _context.ReceptionDocuments.Update(doc);

    public void Delete(ReceptionDocument doc) => _context.ReceptionDocuments.Remove(doc);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}