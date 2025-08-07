using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Interfaces;

public interface IReceptionDocumentRepository
{
    Task<List<ReceptionDocument>> GetAllAsync();
    Task<ReceptionDocument?> GetByIdAsync(long id);
    Task<bool> ExistsByNumberAsync(string number);
    Task<ReceptionDocument?> GetWithItemsAsync(long id);
    Task AddAsync(ReceptionDocument doc);
    void Update(ReceptionDocument doc);
    void Delete(ReceptionDocument doc);
    Task<bool> SaveChangesAsync();
}
