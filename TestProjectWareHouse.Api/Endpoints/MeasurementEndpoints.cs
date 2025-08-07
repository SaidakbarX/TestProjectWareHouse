using TestProjectWareHouse.Application.Dtos;
using TestProjectWareHouse.Application.Services;

namespace TestProjectWareHouse.Api.Endpoints;



public static class MeasurementEndpoints
{
    public static RouteGroupBuilder MapMeasurementEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMeasurementService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        group.MapGet("/archived", async (IMeasurementService service) =>
        {
            return Results.Ok(await service.GetArchivedAsync());
        });

        group.MapGet("/active", async (IMeasurementService service) =>
        {
            return Results.Ok(await service.GetActiveAsync());
        });


        group.MapGet("/{id:long}", async (long id, IMeasurementService service) =>
        {
            var measurement = await service.GetByIdAsync(id);
            return measurement is not null ? Results.Ok(measurement) : Results.NotFound();
        });

        group.MapPost("/", async (MeasurementCreateDto dto, IMeasurementService service) =>
        {
            await service.CreateAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/{id:long}", async (long id, MeasurementUpdateDto dto, IMeasurementService service) =>
        {
            dto.Id = id;
            await service.UpdateAsync(dto);
            return Results.Ok();
        });



        group.MapDelete("/{id:long}", async (long id, IMeasurementService service) =>
        {
            await service.DeleteAsync(id);
            return Results.Ok();
        });

        return group;
    }
}
