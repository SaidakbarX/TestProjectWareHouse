using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Interfaces;

public interface IMeasurementRepository
{
    Task<List<Measurement>> GetAllAsync();
    Task<List<Measurement>> GetArchivedAsync();
    Task<List<Measurement>> GetActiveAsync();
    Task<Measurement?> GetByIdAsync(long id);
    Task<bool> ExistsByNameAsync(string name);
    Task AddAsync(Measurement measurement);
    void Update(Measurement measurement);
    void Delete(Measurement measurement);
    Task<bool> SaveChangesAsync();
}