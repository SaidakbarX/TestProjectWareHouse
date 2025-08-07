using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Interfaces;

public interface IShipmentDocumentRepository
{
    Task<List<ShipmentDocument>> GetAllAsync();
    Task<ShipmentDocument?> GetByIdAsync(long id);
    Task<bool> ExistsByNumberAsync(string number);
    Task<ShipmentDocument?> GetWithItemsAsync(long id);
    Task AddAsync(ShipmentDocument doc);
    void Update(ShipmentDocument doc);
    void Delete(ShipmentDocument doc);
    Task<bool> SaveChangesAsync();
}