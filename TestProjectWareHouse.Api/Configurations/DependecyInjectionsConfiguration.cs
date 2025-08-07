using TestProjectWareHouse.Application.Interfaces;
using TestProjectWareHouse.Application.Services;
using TestProjectWareHouse.Infrastructure.Persistance.Repositories;

namespace TestProjectWareHouse.Api.Configurations;

public static class DependecyInjectionsConfiguration
{
    public static void ConfigureDependecies(this IServiceCollection services)
    {
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddScoped<IReceptionDocumentRepository, ReceptionDocumentRepository>();
        services.AddScoped<IShipmentDocumentRepository, ShipmentDocumentRepository>();
        services.AddScoped<IBalanceRepository, BalanceRepository>();

        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IMeasurementService, MeasurementService>();
        services.AddScoped<IReceptionDocumentService, ReceptionDocumentService>();
        services.AddScoped<IShipmentDocumentService, ShipmentDocumentService>();
        services.AddScoped<IBalanceService, BalanceService>();
    }
}
