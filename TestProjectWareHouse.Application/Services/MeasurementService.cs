using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _repository;

    public MeasurementService(IMeasurementRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MeasurementDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(m => new MeasurementDto
        {
            Id = m.Id,
            Name = m.Name,
            IsArchived = m.IsArchived
        }).ToList();
    }

    public async Task<List<MeasurementDto>> GetArchivedAsync()
    {
        var list = await _repository.GetArchivedAsync();
        return list.Select(m => new MeasurementDto { Id = m.Id, Name = m.Name, IsArchived = m.IsArchived }).ToList();
    }

    public async Task<List<MeasurementDto>> GetActiveAsync()
    {
        var list = await _repository.GetActiveAsync();
        return list.Select(m => new MeasurementDto { Id = m.Id, Name = m.Name, IsArchived = m.IsArchived }).ToList();
    }


    public async Task<MeasurementDto?> GetByIdAsync(long id)
    {
        var measurement = await _repository.GetByIdAsync(id);
        return measurement == null ? null : new MeasurementDto
        {
            Id = measurement.Id,
            Name = measurement.Name,
            IsArchived = measurement.IsArchived
        };
    }

    public async Task CreateAsync(MeasurementCreateDto dto)
    {
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new InvalidOperationException("Measurement with the same name already exists.");

        var entity = new Measurement { Name = dto.Name, IsArchived = dto.IsArchived };
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(MeasurementUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id)
                     ?? throw new KeyNotFoundException("Measurement not found");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            entity.Name = dto.Name;

        entity.IsArchived = dto.IsArchived;

        _repository.Update(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException("Measurement not found");

        _repository.Delete(entity);
        await _repository.SaveChangesAsync();
    }
}
