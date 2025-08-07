using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public interface IShipmentDocumentService
{
    Task<List<ShipmentDocumentDto>> GetAllAsync();
    Task<ShipmentDocumentDto?> GetByIdAsync(long id);
    Task CreateAsync(ShipmentDocumentCreateDto dto);
    Task UpdateAsync(ShipmentDocumentUpdateDto dto);
    Task ChangeStatusAsync(long id, ShipmentStatus newStatus);
    Task DeleteAsync(long id);
}
