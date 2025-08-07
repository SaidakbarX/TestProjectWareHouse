using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Interfaces;

public interface IBalanceRepository
{
    Task<List<Balance>> GetAllAsync();
    Task<Balance?> GetByIdAsync(long id);
    Task<Balance?> GetByResourceAndMeasurementAsync(long resourceId, long measurementId);
    Task AddAsync(Balance balance);
    void Update(Balance balance);
    void Delete(Balance balance);
    Task<bool> SaveChangesAsync();
}