using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public class ShipmentDocumentService : IShipmentDocumentService
{
    private readonly IShipmentDocumentRepository _repository;
    private readonly IBalanceService _balanceService;

    public ShipmentDocumentService(IShipmentDocumentRepository repository, IBalanceService balanceService)
    {
        _repository = repository;
        _balanceService = balanceService;
    }

    public async Task<List<ShipmentDocumentDto>> GetAllAsync()
    {
        var docs = await _repository.GetAllAsync();
        return docs.Select(d => new ShipmentDocumentDto
        {
            Id = d.Id,
            Number = d.Number,
            Date = d.Date,
            ClientId = d.ClientId,
            ClientName = d.Client.Name,
            Status = d.Status,
            Items = d.Items.Select(i => new ShipmentItemDto
            {
                Id = i.Id,
                MeasurementName = i.Measurement.Name,
                ResourceName = i.Resource.Name,
                ResourceId = i.ResourceId,
                MeasurementId = i.MeasurementId,
                Quantity = i.Quantity
            }).ToList()
        }).ToList();
    }

    public async Task<ShipmentDocumentDto?> GetByIdAsync(long id)
    {
        var doc = await _repository.GetWithItemsAsync(id);
        return doc == null ? null : new ShipmentDocumentDto
        {
            Id = doc.Id,
            Number = doc.Number,
            Date = doc.Date,
            ClientId = doc.ClientId,
            Status = doc.Status,
            Items = doc.Items.Select(i => new ShipmentItemDto
            {
                Id = i.Id,
                ResourceId = i.ResourceId,
                MeasurementId = i.MeasurementId,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task CreateAsync(ShipmentDocumentCreateDto dto)
    {
        if (await _repository.ExistsByNumberAsync(dto.Number))
            throw new InvalidOperationException("Document with the same number already exists.");

        var document = new ShipmentDocument
        {
            Number = dto.Number,
            Date = dto.Date,
            ClientId = dto.ClientId,
            Status = ShipmentStatus.Draft,
            Items = dto.Items.Select(i => new ShipmentItem
            {
                ResourceId = i.ResourceId,
                MeasurementId = i.MeasurementId,
                Quantity = i.Quantity
            }).ToList()
        };

        await _repository.AddAsync(document);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(ShipmentDocumentUpdateDto dto)
    {
        var document = await _repository.GetWithItemsAsync(dto.Id)
                       ?? throw new KeyNotFoundException("Document not found");

        document.Number = dto.Number;
        document.Date = dto.Date;
        document.ClientId = dto.ClientId;
        document.Items = dto.Items.Select(i => new ShipmentItem
        {
            Id = i.Id,
            ResourceId = i.ResourceId,
            MeasurementId = i.MeasurementId,
            Quantity = i.Quantity
        }).ToList();

        _repository.Update(document);
        await _repository.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(long id, ShipmentStatus newStatus)
    {
        var document = await _repository.GetWithItemsAsync(id)
                       ?? throw new KeyNotFoundException("Document not found");

        if (document.Status == newStatus)
            return;

        if (newStatus == ShipmentStatus.Signed)
        {
            foreach (var item in document.Items)
                await _balanceService.DecreaseBalanceAsync(item.ResourceId, item.MeasurementId, item.Quantity);
        }
        else if (newStatus == ShipmentStatus.Revoked && document.Status == ShipmentStatus.Signed)
        {
            foreach (var item in document.Items)
                await _balanceService.IncreaseBalanceAsync(item.ResourceId, item.MeasurementId, item.Quantity);
        }

        document.Status = newStatus;
        _repository.Update(document);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var document = await _repository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException("Document not found");

        if (document.Status == ShipmentStatus.Signed)
            throw new InvalidOperationException("Cannot delete signed document.");

        _repository.Delete(document);
        await _repository.SaveChangesAsync();
    }
}
