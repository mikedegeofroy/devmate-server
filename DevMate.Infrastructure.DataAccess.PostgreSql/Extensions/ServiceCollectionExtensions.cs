using DevMate.Application.Abstractions.Repositories;
using DevMate.Infrastructure.DataAccess.PostgreSql.DataSources;
using DevMate.Infrastructure.DataAccess.PostgreSql.Migrations;
using DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccessPostgreSql(
        this IServiceCollection collection,
        string connectionString)
    {
        collection.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Initial).Assembly).For.Migrations())
            .BuildServiceProvider(false);

        collection.AddSingleton<IEventRepository, EventRepository>();
        collection.AddSingleton<IUserRepository, UserRepository>();
        collection.AddSingleton<IWaitlistRepository, WaitlistRepository>();
        collection.AddSingleton<IAuthorizedUsersRepository, AuthorizedUsersRepository>();
        collection.AddSingleton<SqlDataAccess>();

        return collection;
    }
}