using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Services;

namespace TestProjectWareHouse.Api.Endpoints;



public static class ClientEndpoints
{
    public static RouteGroupBuilder MapClientEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IClientService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        group.MapGet("/archived", async (IClientService service) =>
        {
            return Results.Ok(await service.GetArchivedAsync());
        });

        group.MapGet("/active", async (IClientService service) =>
        {
            return Results.Ok(await service.GetActiveAsync());
        });


        group.MapGet("/{id:long}", async (long id, IClientService service) =>
        {
            var client = await service.GetByIdAsync(id);
            return client is not null ? Results.Ok(client) : Results.NotFound();
        });

        group.MapPost("/", async (ClientCreateDto dto, IClientService service) =>
        {
            await service.CreateAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/{id:long}",async(long id,ClientUpdateDto dto, IClientService service) =>
        {
            dto.Id = id;
            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapDelete("/{id:long}", async (long id, IClientService service) =>
        {
            await service.DeleteAsync(id);
            return Results.Ok();
        });

        return group;
    }
}
