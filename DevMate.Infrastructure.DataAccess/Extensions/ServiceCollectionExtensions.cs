using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;
using ParkingApp.Application.Abstractions.Repositories;
using ParkingApp.Infrastructure.DataAccess.Repositories;
using Workshop5.Infrastructure.DataAccess.Migrations;

namespace ParkingApp.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration
        )
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddScoped<IParkingRepository, ParkingRepository>();
        
        collection.AddPlatformMigrations(typeof(Initial).Assembly);
        return collection;
    }
    
    public static Task UseInfrastructureDataAccessAsync(this IServiceScope scope, CancellationToken cancellationToken)
    {
        return scope.UsePlatformMigrationsAsync(cancellationToken);
    }
}