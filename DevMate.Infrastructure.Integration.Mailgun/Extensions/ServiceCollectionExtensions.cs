using DevMate.Application.Abstractions.Services;
using DevMate.Infrastructure.Integration.Mailgun.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.Integration.Mailgun.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureIntegrationMailgun(this IServiceCollection collection)
    {
        collection.AddScoped<IMailService, MockMailService>();

        return collection;
    }
}