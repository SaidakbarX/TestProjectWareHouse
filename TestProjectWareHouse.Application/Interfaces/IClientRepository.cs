using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<List<Client>> GetArchivedAsync();
    Task<List<Client>> GetActiveAsync();
    Task<Client?> GetByIdAsync(long id);
    Task<bool> ExistsByNameAsync(string name);
    Task AddAsync(Client client);
    void Update(Client client);
    void Delete(Client client);
    Task<bool> SaveChangesAsync();
}