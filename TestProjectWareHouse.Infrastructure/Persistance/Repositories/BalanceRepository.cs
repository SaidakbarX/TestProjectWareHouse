using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly AppDbContext _context;

    public BalanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Balance>> GetAllAsync() => await _context.Balances.Include(x=>x.Resource).Include(x=>x.Measurement).ToListAsync();

    public async Task<Balance?> GetByIdAsync(long id) => await _context.Balances.FindAsync(id);

    public async Task<Balance?> GetByResourceAndMeasurementAsync(long resourceId, long measurementId) =>
        await _context.Balances.FirstOrDefaultAsync(b =>
            b.ResourceId == resourceId && b.MeasurementId == measurementId);

    public async Task AddAsync(Balance balance) => await _context.Balances.AddAsync(balance);

    public void Update(Balance balance) => _context.Balances.Update(balance);

    public void Delete(Balance balance) => _context.Balances.Remove(balance);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}