using TestProjectWareHouse.Application.Dtos;

namespace TestProjectWareHouse.Application.Services;

public interface IClientService
{
    Task<List<ClientDto>> GetAllAsync();
    Task<List<ClientDto>> GetArchivedAsync();
    Task<List<ClientDto>> GetActiveAsync();
    Task<ClientDto?> GetByIdAsync(long id);
    Task CreateAsync(ClientCreateDto dto);
    Task UpdateAsync(ClientUpdateDto dto);
    Task DeleteAsync(long id);
}