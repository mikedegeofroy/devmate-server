using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Abstractions.Telegram.Services.UserClients;
using DevMate.Infrastructure.Integration.Telegram.User.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.Integration.Telegram.User.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureIntegrationTelegram(this IServiceCollection collection)
    {
        collection.AddScoped<ITelegramUserClientFactory, TelegramUserClientFactory>();

        return collection;
    }
}