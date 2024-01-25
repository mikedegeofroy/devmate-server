using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Infrastructure.Integration.Telegram.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.Integration.Telegram.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureIntegrationTelegram(this IServiceCollection collection)
    {
        collection.AddScoped<ITelegramClientService, TelegramClientService>();

        return collection;
    }
}