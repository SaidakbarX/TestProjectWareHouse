
using TestProjectWareHouse.Api.Configurations;
using TestProjectWareHouse.Api.Endpoints;
using TestProjectWareHouse.Infrastructure.Persistance;

namespace TestProjectWareHouse.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost5173", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:4200",
                        "http://localhost:5173"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            builder.ConfigureDataBase();
            builder.Services.ConfigureDependecies();


            var app = builder.Build();

            app.UseCors("AllowLocalhost5173");
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGroup("/api/resources")
            .MapResourceEndpoints();

            app.MapGroup("/api/clients")
            .MapClientEndpoints();

            app.MapGroup("/api/measurements")
            .MapMeasurementEndpoints();

            app.MapGroup("/api/receptions")
            .MapReceptionDocumentEndpoints();

            app.MapGroup("/api/shipments")
            .MapShipmentDocumentEndpoints();

            app.MapGroup("/api/balances")
            .MapBalanceEndpoints();

            app.MapControllers();

            app.Run();
        }
    }
}
