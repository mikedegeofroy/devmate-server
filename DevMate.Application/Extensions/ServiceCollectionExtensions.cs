using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Contracts.Auth;
using DevMate.Application.Contracts.Mailing;
using DevMate.Application.Services;
using DevMate.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ITelegramAnalyticsService, TelegramAnalyticsService>();
        collection.AddScoped<IMailingService, MailingService>();
        collection.AddScoped<IAuthService, TelegramUserAuthService>();
        collection.AddScoped<IEventService, EventService>();

        return collection;
    }
}