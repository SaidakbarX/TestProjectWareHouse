using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Services;
using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Api.Endpoints;



public static class ShipmentDocumentEndpoints
{
    public static RouteGroupBuilder MapShipmentDocumentEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IShipmentDocumentService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        group.MapGet("/{id:long}", async (long id, IShipmentDocumentService service) =>
        {
            var doc = await service.GetByIdAsync(id);
            return doc is not null ? Results.Ok(doc) : Results.NotFound();
        });

        group.MapPost("/", async (ShipmentDocumentCreateDto dto, IShipmentDocumentService service) =>
        {
            await service.CreateAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/", async (ShipmentDocumentUpdateDto dto, IShipmentDocumentService service) =>
        {
            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapPatch("/{id:long}/status/{status}", async (long id, ShipmentStatus status, IShipmentDocumentService service) =>
        {
            await service.ChangeStatusAsync(id, status);
            return Results.Ok();
        });

        group.MapDelete("/{id:long}", async (long id, IShipmentDocumentService service) =>
        {
            await service.DeleteAsync(id);
            return Results.Ok();
        });

        return group;
    }
}
