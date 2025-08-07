using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public class BalanceService : IBalanceService
{
    private readonly IBalanceRepository _repository;

    public BalanceService(IBalanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BalanceDto>> GetAllAsync()
    {
        var balances = await _repository.GetAllAsync();
        return balances.Select(b => new BalanceDto
        {
            Id = b.Id,
            ResourceId = b.ResourceId,
            MeasurementName = b.Measurement.Name,
            ResourceName = b.Resource.Name,
            MeasurementId = b.MeasurementId,
            Quantity = b.Quantity
        }).ToList();
    }

    public async Task<BalanceDto?> GetByIdAsync(long id)
    {
        var balance = await _repository.GetByIdAsync(id);
        return balance == null ? null : new BalanceDto
        {
            Id = balance.Id,
            ResourceId = balance.ResourceId,
            MeasurementId = balance.MeasurementId,
            Quantity = balance.Quantity
        };
    }

    public async Task IncreaseBalanceAsync(long resourceId, long measurementId, long quantity)
    {
        var balance = await _repository.GetByResourceAndMeasurementAsync(resourceId, measurementId);
        if (balance == null)
        {
            balance = new Balance
            {
                ResourceId = resourceId,
                MeasurementId = measurementId,
                Quantity = quantity
            };
            await _repository.AddAsync(balance);
        }
        else
        {
            balance.Quantity += quantity;
            _repository.Update(balance);
        }
        await _repository.SaveChangesAsync();
    }

    public async Task DecreaseBalanceAsync(long resourceId, long measurementId, long quantity)
    {
        var balance = await _repository.GetByResourceAndMeasurementAsync(resourceId, measurementId)
                      ?? throw new InvalidOperationException("No balance found for this resource and measurement.");

        if (balance.Quantity < quantity)
            throw new InvalidOperationException("Not enough resources in stock.");

        balance.Quantity -= quantity;

        if (balance.Quantity == 0)
        {
            _repository.Delete(balance);
        }
        else
        {
            _repository.Update(balance);
        }

        await _repository.SaveChangesAsync();
    }

}
