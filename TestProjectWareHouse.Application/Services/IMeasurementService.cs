using TestProjectWareHouse.Application.Dtos;

namespace TestProjectWareHouse.Application.Services;

public interface IMeasurementService
{
    Task<List<MeasurementDto>> GetAllAsync();
    Task<List<MeasurementDto>> GetArchivedAsync();
    Task<List<MeasurementDto>> GetActiveAsync();
    Task<MeasurementDto?> GetByIdAsync(long id);
    Task CreateAsync(MeasurementCreateDto dto);
    Task UpdateAsync(MeasurementUpdateDto dto);
    Task DeleteAsync(long id);
}
