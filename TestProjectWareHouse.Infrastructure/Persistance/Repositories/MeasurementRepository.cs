using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly AppDbContext _context;

    public MeasurementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Measurement>> GetAllAsync() => await _context.Measurements.ToListAsync();
    public async Task<List<Measurement>> GetArchivedAsync() =>
    await _context.Measurements.Where(m => m.IsArchived).ToListAsync();

    public async Task<List<Measurement>> GetActiveAsync() =>
        await _context.Measurements.Where(m => !m.IsArchived).ToListAsync();


    public async Task<Measurement?> GetByIdAsync(long id) => await _context.Measurements.FindAsync(id);

    public async Task<bool> ExistsByNameAsync(string name) =>
        await _context.Measurements.AnyAsync(m => m.Name == name);

    public async Task AddAsync(Measurement measurement) =>
        await _context.Measurements.AddAsync(measurement);

    public void Update(Measurement measurement) => _context.Measurements.Update(measurement);

    public void Delete(Measurement measurement) => _context.Measurements.Remove(measurement);
    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}