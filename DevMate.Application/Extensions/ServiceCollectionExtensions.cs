using Microsoft.Extensions.DependencyInjection;
using DevMate.Application.Analytics;
using DevMate.Application.Auth;
using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Contracts.Auth;
using DevMate.Application.Contracts.Mailing;

namespace DevMate.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ITelegramAnalyticsService, TelegramAnalyticsService>();
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddScoped<IMailingService, MailingService.MailingService>();

        return collection;
    }
}