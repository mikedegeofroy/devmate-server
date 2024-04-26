using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Extensions;

public static class HostExtensions
{
    public static IHost MigrateUp(this IHost app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        return app;
    }
}