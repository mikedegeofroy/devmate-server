using DevMate.Infrastructure.Integration.Telegram.Services;
using Microsoft.Extensions.DependencyInjection;
using DevMate.Application.Abstractions.Telegram.Services;

namespace DevMate.Infrastructure.Integration.Telegram.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTelegramIntegration(this IServiceCollection collection)
    {
        collection.AddSingleton<ITelegramService, CachingTelegramService>();

        return collection;
    }
}