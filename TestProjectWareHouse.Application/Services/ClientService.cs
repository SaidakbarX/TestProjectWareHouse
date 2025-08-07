using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ClientDto>> GetAllAsync()
    {
        var clients = await _repository.GetAllAsync();
        return clients.Select(c => new ClientDto
        {
            Id = c.Id,
            Name = c.Name,
            Address = c.Address,
            IsArchived = c.IsArchived
        }).ToList();
    }

    public async Task<List<ClientDto>> GetArchivedAsync()
    {
        var clients = await _repository.GetArchivedAsync();
        return clients.Select(c => new ClientDto { Id = c.Id, Name = c.Name, Address = c.Address, IsArchived = c.IsArchived }).ToList();
    }

    public async Task<List<ClientDto>> GetActiveAsync()
    {
        var clients = await _repository.GetActiveAsync();
        return clients.Select(c => new ClientDto { Id = c.Id, Name = c.Name, Address = c.Address, IsArchived = c.IsArchived }).ToList();
    }



    public async Task<ClientDto?> GetByIdAsync(long id)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client == null) return null;

        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Address = client.Address,
            IsArchived = client.IsArchived
        };
    }

    public async Task CreateAsync(ClientCreateDto dto)
    {
        var client = new Client
        {
            Name = dto.Name,
            Address = dto.Address,
            IsArchived = dto.IsArchived
        };
        await _repository.AddAsync(client);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(ClientUpdateDto dto)
    {
        var client = await _repository.GetByIdAsync(dto.Id);
        if (client == null) throw new KeyNotFoundException("Client not found");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            client.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Address))
            client.Address = dto.Address;

        client.IsArchived = dto.IsArchived;

        _repository.Update(client);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client == null) throw new KeyNotFoundException("Client not found");

        _repository.Delete(client);
        await _repository.SaveChangesAsync();
    }
}
