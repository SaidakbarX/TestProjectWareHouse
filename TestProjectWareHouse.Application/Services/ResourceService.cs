using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _repository;

    public ResourceService(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ResourceDto>> GetAllAsync()
    {
        var resources = await _repository.GetAllAsync();
        return resources.Select(r => new ResourceDto
        {
            Id = r.Id,
            Name = r.Name,
            IsArchived = r.IsArchived
        }).ToList();
    }


    public async Task<List<ResourceDto>> GetArchivedAsync()
    {
        var list = await _repository.GetArchivedAsync();
        return list.Select(r => new ResourceDto { Id = r.Id, Name = r.Name, IsArchived = r.IsArchived }).ToList();
    }

    public async Task<List<ResourceDto>> GetActiveAsync()
    {
        var list = await _repository.GetActiveAsync();
        return list.Select(r => new ResourceDto { Id = r.Id, Name = r.Name, IsArchived = r.IsArchived }).ToList();
    }


    public async Task<ResourceDto?> GetByIdAsync(long id)
    {
        var resource = await _repository.GetByIdAsync(id);
        if (resource == null) return null;

        return new ResourceDto
        {
            Id = resource.Id,
            Name = resource.Name,
            IsArchived = resource.IsArchived
        };
    }

    public async Task CreateAsync(ResourceCreateDto dto)
    {
        var resource = new Resource
        {
            Name = dto.Name,
            IsArchived = dto.IsArchived
        };
        await _repository.AddAsync(resource);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(ResourceUpdateDto dto)
    {
        var resource = await _repository.GetByIdAsync(dto.Id);
        if (resource == null) throw new KeyNotFoundException("Resource not found");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            resource.Name = dto.Name;

        resource.IsArchived = dto.IsArchived;

        _repository.Update(resource);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var resource = await _repository.GetByIdAsync(id);
        if (resource == null) throw new KeyNotFoundException("Resource not found");

        _repository.Delete(resource);
        await _repository.SaveChangesAsync();
    }
}

