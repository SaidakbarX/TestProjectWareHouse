using Microsoft.EntityFrameworkCore;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Infrastructure.Persistance.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> GetAllAsync() => await _context.Clients.ToListAsync();
    public async Task<List<Client>> GetArchivedAsync() =>
    await _context.Clients.Where(c => c.IsArchived).ToListAsync();

    public async Task<List<Client>> GetActiveAsync() =>
        await _context.Clients.Where(c => !c.IsArchived).ToListAsync();


    public async Task<Client?> GetByIdAsync(long id) => await _context.Clients.FindAsync(id);

    public async Task<bool> ExistsByNameAsync(string name) =>
        await _context.Clients.AnyAsync(c => c.Name == name);

    public async Task AddAsync(Client client) => await _context.Clients.AddAsync(client);

    public void Update(Client client) => _context.Clients.Update(client);

    public void Delete(Client client) => _context.Clients.Remove(client);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}