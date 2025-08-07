using TestProjectWareHouse.Application.Services;

namespace TestProjectWareHouse.Api.Endpoints;



public static class BalanceEndpoints
{
    public static RouteGroupBuilder MapBalanceEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IBalanceService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        group.MapGet("/{id:long}", async (long id, IBalanceService service) =>
        {
            var balance = await service.GetByIdAsync(id);
            return balance is not null ? Results.Ok(balance) : Results.NotFound();
        });

        return group;
    }
}
