using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public interface IResourceService
{
    Task<List<ResourceDto>> GetAllAsync();
    Task<List<ResourceDto>> GetArchivedAsync();
    Task<List<ResourceDto>> GetActiveAsync();
    Task<ResourceDto?> GetByIdAsync(long id);
    Task CreateAsync(ResourceCreateDto dto);
    Task UpdateAsync(ResourceUpdateDto dto);
    Task DeleteAsync(long id);
}