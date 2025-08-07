using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public class ReceptionDocumentService : IReceptionDocumentService
{
    private readonly IReceptionDocumentRepository _repository;
    private readonly IBalanceService _balanceService;

    public ReceptionDocumentService(IReceptionDocumentRepository repository, IBalanceService balanceService)
    {
        _repository = repository;
        _balanceService = balanceService;
    }

    public async Task<List<ReceptionDocumentDto>> GetAllAsync()
    {
        var docs = await _repository.GetAllAsync();
        return docs.Select(d => new ReceptionDocumentDto
        {
            Id = d.Id,
            Number = d.Number,
            Date = d.Date,
            Items = d.Items.Select(i => new ReceptionItemDto
            {
                Id = i.Id,
                ResourceId = i.ResourceId,
                ResourceName = i.Resource.Name,
                MeasurementName = i.Measurement.Name,
                MeasurementId = i.MeasurementId,
                Quantity = i.Quantity
            }).ToList()
        }).ToList();
    }

    public async Task<ReceptionDocumentDto?> GetByIdAsync(long id)
    {
        var doc = await _repository.GetWithItemsAsync(id);
        return doc == null ? null : new ReceptionDocumentDto
        {
            Id = doc.Id,
            Number = doc.Number,
            Date = doc.Date,
            Items = doc.Items.Select(i => new ReceptionItemDto
            {
                Id = i.Id,
                ResourceId = i.ResourceId,
                MeasurementId = i.MeasurementId,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task CreateAsync(ReceptionDocumentCreateDto dto)
    {
        if (await _repository.ExistsByNumberAsync(dto.Number))
            throw new InvalidOperationException("Document with the same number already exists.");

        var document = new ReceptionDocument
        {
            Number = dto.Number,
            Date = dto.Date,
            Items = dto.Items.Select(i => new ReceptionItem
            {
                ResourceId = i.ResourceId,
                MeasurementId = i.MeasurementId,
                Quantity = i.Quantity
            }).ToList()
        };

        await _repository.AddAsync(document);
        await _repository.SaveChangesAsync();

        foreach (var item in document.Items)
            await _balanceService.IncreaseBalanceAsync(item.ResourceId, item.MeasurementId, item.Quantity);
    }

    public async Task UpdateAsync(ReceptionDocumentUpdateDto dto)
    {
        var document = await _repository.GetWithItemsAsync(dto.Id)
                       ?? throw new KeyNotFoundException("Document not found");

        foreach (var item in document.Items)
            await _balanceService.DecreaseBalanceAsync(item.ResourceId, item.MeasurementId, item.Quantity);

        document.Number = dto.Number;
        document.Date = dto.Date;
        document.Items = dto.Items.Select(i => new ReceptionItem
        {
            Id = i.Id,
            ResourceId = i.ResourceId,
            MeasurementId = i.MeasurementId,
            Quantity = i.Quantity
        }).ToList();

        _repository.Update(document);
        await _repository.SaveChangesAsync();

        foreach (var item in document.Items)
            await _balanceService.IncreaseBalanceAsync(item.ResourceId, item.MeasurementId, item.Quantity);
    }

    public async Task DeleteAsync(long id)
    {
        var document = await _repository.GetWithItemsAsync(id)
                       ?? throw new KeyNotFoundException("Document not found");

        foreach (var item in document.Items)
            await _balanceService.DecreaseBalanceAsync(item.ResourceId, item.MeasurementId, item.Quantity);

        _repository.Delete(document);
        await _repository.SaveChangesAsync();
    }
}
