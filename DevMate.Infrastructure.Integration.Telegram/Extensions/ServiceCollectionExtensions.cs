using DevMate.Application.Abstractions.Services;
using DevMate.Application.Abstractions.Telegram.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.Integration.Telegram.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureIntegrationTelegram(this IServiceCollection collection)
    {
        collection.AddSingleton<ITelegramBot, TelegramBot>();
        collection.AddSingleton<IEventPublisher, TelegramBot>();

        return collection;
    }
}