using TestProjectWareHouse.Application.Dtos;

namespace TestProjectWareHouse.Application.Services;

public interface IBalanceService
{
    Task<List<BalanceDto>> GetAllAsync();
    Task<BalanceDto?> GetByIdAsync(long id);
    Task IncreaseBalanceAsync(long resourceId, long measurementId, long quantity);
    Task DecreaseBalanceAsync(long resourceId, long measurementId, long quantity);
}
