using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Services;

namespace TestProjectWareHouse.Api.Endpoints;



public static class ReceptionDocumentEndpoints
{
    public static RouteGroupBuilder MapReceptionDocumentEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IReceptionDocumentService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        group.MapGet("/{id:long}", async (long id, IReceptionDocumentService service) =>
        {
            var doc = await service.GetByIdAsync(id);
            return doc is not null ? Results.Ok(doc) : Results.NotFound();
        });

        group.MapPost("/", async (ReceptionDocumentCreateDto dto, IReceptionDocumentService service) =>
        {
            await service.CreateAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/{id}", async (long id, ReceptionDocumentUpdateDto dto, IReceptionDocumentService service) =>
        {
            dto.Id = id;
            await service.UpdateAsync(dto);
            return Results.Ok();
        });


        group.MapDelete("/{id:long}", async (long id, IReceptionDocumentService service) =>
        {
            await service.DeleteAsync(id);
            return Results.Ok();
        });

        return group;
    }
}
