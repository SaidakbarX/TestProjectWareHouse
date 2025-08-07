using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Services;

namespace TestProjectWareHouse.Api.Endpoints;



public static class ResourceEndpoints
{
    public static RouteGroupBuilder MapResourceEndpoints(this RouteGroupBuilder group)
    {


        group.MapGet("/", async (IResourceService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        group.MapGet("/archived", async (IResourceService service) =>
        {
            return Results.Ok(await service.GetArchivedAsync());
        });

        group.MapGet("/active", async (IResourceService service) =>
        {
            return Results.Ok(await service.GetActiveAsync());
        });


        group.MapGet("/{id:long}", async (long id, IResourceService service) =>
        {
            var resource = await service.GetByIdAsync(id);
            return resource is not null ? Results.Ok(resource) : Results.NotFound();
        });

        group.MapPost("/", async (ResourceCreateDto dto, IResourceService service) =>
        {
            await service.CreateAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/{id:long}", async (long id, ResourceUpdateDto dto, IResourceService service) =>
        {
            dto.Id = id;
            await service.UpdateAsync(dto);
            return Results.Ok();
        });


        group.MapDelete("/{id:long}", async (long id, IResourceService service) =>
        {
            await service.DeleteAsync(id);
            return Results.Ok();
        });

        return group;
    }
}

