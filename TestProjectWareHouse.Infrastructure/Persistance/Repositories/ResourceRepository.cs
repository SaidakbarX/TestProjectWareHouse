using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly AppDbContext _context;

    public ResourceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Resource>> GetAllAsync() => await _context.Resources.ToListAsync();
    public async Task<List<Resource>> GetArchivedAsync() =>
    await _context.Resources.Where(r => r.IsArchived).ToListAsync();

    public async Task<List<Resource>> GetActiveAsync() =>
        await _context.Resources.Where(r => !r.IsArchived).ToListAsync();


    public async Task<Resource?> GetByIdAsync(long id) => await _context.Resources.FindAsync(id);

    public async Task<Resource?> GetByNameAsync(string name) =>
        await _context.Resources.FirstOrDefaultAsync(r => r.Name == name);

    public async Task<bool> ExistsByNameAsync(string name) =>
        await _context.Resources.AnyAsync(r => r.Name == name);

    public async Task AddAsync(Resource resource) => await _context.Resources.AddAsync(resource);

    public void Update(Resource resource) => _context.Resources.Update(resource);

    public void Delete(Resource resource) => _context.Resources.Remove(resource);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}
