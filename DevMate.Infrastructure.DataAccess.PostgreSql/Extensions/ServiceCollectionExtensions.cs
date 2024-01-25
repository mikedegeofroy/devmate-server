using DevMate.Application.Abstractions.Repositories;
using DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccessPostgreSql(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddSingleton<IEventRepository, EventRepository>();

        return collection;
    }
}