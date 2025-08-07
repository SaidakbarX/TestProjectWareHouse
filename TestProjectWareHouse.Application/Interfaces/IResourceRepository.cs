using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Interfaces;

public interface IResourceRepository
{
    Task<List<Resource>> GetAllAsync();
    Task<List<Resource>> GetArchivedAsync();
    Task<List<Resource>> GetActiveAsync();
    Task<Resource?> GetByIdAsync(long id);
    Task<Resource?> GetByNameAsync(string name);
    Task<bool> ExistsByNameAsync(string name);
    Task AddAsync(Resource resource);
    void Update(Resource resource);
    void Delete(Resource resource);
    Task<bool> SaveChangesAsync();
}
