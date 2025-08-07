using TestProjectWareHouse.Application.Dtos;

namespace TestProjectWareHouse.Application.Services;

public interface IReceptionDocumentService
{
    Task<List<ReceptionDocumentDto>> GetAllAsync();
    Task<ReceptionDocumentDto?> GetByIdAsync(long id);
    Task CreateAsync(ReceptionDocumentCreateDto dto);
    Task UpdateAsync(ReceptionDocumentUpdateDto dto);
    Task DeleteAsync(long id);
}
