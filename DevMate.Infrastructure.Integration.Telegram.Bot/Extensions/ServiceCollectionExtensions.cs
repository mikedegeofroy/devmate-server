using DevMate.Infrastructure.Integration.Telegram.Bot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.Integration.Telegram.Bot.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureTelegramBotService(this IServiceCollection collection)
    {
        collection.AddScoped<TelegramBotClientFactory, TelegramBotClientFactory>();

        return collection;
    }
}